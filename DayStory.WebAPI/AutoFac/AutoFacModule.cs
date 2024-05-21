using Autofac;
using DayStory.Application.Interfaces;
using DayStory.Application.Mappers;
using DayStory.Application.Services;
using DayStory.Domain.Repositories;
using DayStory.Infrastructure.Data.Context;
using DayStory.Infrastructure.Repositories;
using System.Reflection;
using Module = Autofac.Module;

namespace MovieAPI.WebAPI.AutoFac;

public class AutoFacModule : Module
{
    protected override void Load(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();

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
    }
}
