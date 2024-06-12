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

        containerBuilder.Register(context =>
        {
            var config = context.Resolve<IConfiguration>();
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
                cfg.AddProfile(new UserMapperProfile());
                cfg.AddProfile(new EventMapperProfile());
                cfg.AddProfile(new DaySummaryMapperProfile(config));
            });
        }).AsSelf().SingleInstance();

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

        // OpenAI
        containerBuilder.Register(context =>
        {
            var configuration = context.Resolve<IConfiguration>();
            var apiKey = configuration["OpenAI:ApiKey"];

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentNullException("OpenAI:ApiKey", "API key is not configured. Please check your configuration.");
            }

            var httpClientFactory = context.Resolve<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient();
            return new OpenAIService(httpClient, apiKey);
        }).As<IOpenAIService>().SingleInstance();
    }
}
