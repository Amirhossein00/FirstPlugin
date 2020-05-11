using System;
using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Projects.Data;
using Nop.Plugin.Projects.Domain;
using Nop.Plugin.Projects.Factory;
using Nop.Plugin.Projects.Services;
using Nop.Web.Framework.Infrastructure.Extensions;

namespace Nop.Plugin.Projects.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
       
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<ProjectService>().As<IProjectService>().InstancePerLifetimeScope();

            builder.RegisterType<ProjectModelFactory>().As<IProjectModelFactory>().InstancePerLifetimeScope();

            builder.RegisterPluginDataContext<ProjectObjectContext>("nop_object_Context_project");

            builder.RegisterType<EfRepository<Project>>().As<IRepository<Project>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_Context_project"))
                .InstancePerLifetimeScope();
        }

        public int Order => 1;
    }
}
