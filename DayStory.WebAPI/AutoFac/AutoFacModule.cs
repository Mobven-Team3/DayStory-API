using Autofac;
using AutoMapper;
using DayStory.Application.Auth;
using DayStory.Application.Interfaces;
using DayStory.Application.Mappers;
using DayStory.Application.Services;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;
using DayStory.Infrastructure.Data.Context;
using DayStory.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using StackExchange.Redis;
using System.Reflection;
using Module = Autofac.Module;

namespace MovieAPI.WebAPI.AutoFac;

public class AutoFacModule : Module
{
    protected override void Load(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterGeneric(typeof(GenericRepository<,>)).As(typeof(IGenericRepository<,>)).InstancePerLifetimeScope();

        containerBuilder.RegisterType<AuthHelper>().As<IAuthHelper>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<PasswordHasher<User>>().As<IPasswordHasher<User>>().InstancePerLifetimeScope();

        // AutoMapper
        containerBuilder.Register(context => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UserMapperProfile>();
            cfg.AddProfile<EventMapperProfile>();
        })).AsSelf().SingleInstance();

        containerBuilder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();

        var apiAssembly = Assembly.GetExecutingAssembly();
        var repoAssembly = Assembly.GetAssembly(typeof(DayStoryAPIDbContext));
        var serviceAssembly = Assembly.GetAssembly(typeof(MapperProfile));

        containerBuilder.RegisterAssemblyTypes(apiAssembly, repoAssembly)
            .Where(x => x.Name.EndsWith("Repository"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        containerBuilder.RegisterGeneric(typeof(BaseService<,>)).As(typeof(IBaseService<,>)).InstancePerLifetimeScope();

        containerBuilder.RegisterAssemblyTypes(serviceAssembly, apiAssembly)
            .Where(x => x.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        // Redis IDistributedCache
        containerBuilder.Register(c =>
        {
            var config = c.Resolve<IConfiguration>();
            var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"), true);
            return ConnectionMultiplexer.Connect(options);
        }).As<IConnectionMultiplexer>().SingleInstance();

        containerBuilder.Register(c => new RedisCache(new RedisCacheOptions
        {
            Configuration = c.Resolve<IConfiguration>().GetConnectionString("Redis")
        })).As<IDistributedCache>().InstancePerLifetimeScope();

        containerBuilder.RegisterType<CacheService>().As<ICacheService>().InstancePerLifetimeScope();
    }
}
