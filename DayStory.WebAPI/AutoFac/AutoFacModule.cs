using Autofac;
using AutoMapper;
using DayStory.Application.Interfaces;
using DayStory.Application.Mappers;
using DayStory.Application.Options;
using DayStory.Application.Services;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;
using DayStory.Infrastructure.Data.Context;
using DayStory.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Reflection;
using Module = Autofac.Module;

namespace MovieAPI.WebAPI.AutoFac;

public class AutoFacModule : Module
{
    protected override void Load(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterGeneric(typeof(GenericRepository<,>)).As(typeof(IGenericRepository<,>)).InstancePerLifetimeScope();

        containerBuilder.RegisterType<AuthService>().As<IAuthService>().InstancePerLifetimeScope();
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

        // OpenAI configuration
        containerBuilder.Register(context =>
        {
            var configuration = context.Resolve<IConfiguration>();
            var openAIOptions = new OpenAIOptions();
            configuration.GetSection("OpenAI").Bind(openAIOptions);

            if (string.IsNullOrWhiteSpace(openAIOptions.ApiKey))
            {
                throw new ArgumentNullException("OpenAI:ApiKey", "API key is not configured. Please check your configuration.");
            }

            var httpClientFactory = context.Resolve<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient();
            return new OpenAIService(httpClient, Options.Create(openAIOptions));
        }).As<IOpenAIService>().SingleInstance();
    }
}
