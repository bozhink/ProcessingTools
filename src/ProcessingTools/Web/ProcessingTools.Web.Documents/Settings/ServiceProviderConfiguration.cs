// <copyright file="ServiceProviderConfiguration.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using System;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ProcessingTools.Configuration.Autofac;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.IO;
    using ProcessingTools.Contracts.Services.Meta;
    using ProcessingTools.Contracts.Web.Services;
    using ProcessingTools.Web.Models.Shared;
    using ProcessingTools.Web.Services;

    /// <summary>
    /// Service provider configuration.
    /// </summary>
    public static class ServiceProviderConfiguration
    {
        /// <summary>
        /// Build DI container.
        /// </summary>
        /// <param name="services">Service collection to be configured.</param>
        /// <param name="configuration">Application configuration.</param>
        /// <returns>Configured service collection.</returns>
        public static IContainer BuildContainer(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            // Add bindings
            builder.Populate(services);

            builder.RegisterType<ApplicationContextFactory>().AsSelf().InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<ApplicationContextFactory>().ApplicationContext).As<IApplicationContext>().InstancePerDependency();
            builder.Register(c => c.Resolve<IApplicationContext>().UserContext).As<IUserContext>().InstancePerDependency();

            builder.RegisterType<ProcessingTools.Services.Meta.JatsArticleMetaHarvester>().As<IJatsArticleMetaHarvester>().InstancePerDependency();
            builder.RegisterType<ProcessingTools.Services.IO.XmlReadService>().As<IXmlReadService>().InstancePerDependency();

            builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerDependency();

            builder.RegisterInstance(ProcessingTools.Common.Constants.Defaults.Encoding).As<System.Text.Encoding>().SingleInstance();

            builder.RegisterModule(new XmlTransformersAutofacModule
            {
                Configuration = configuration,
            });
            builder.RegisterModule<TransformersFactoriesAutofacModule>();
            builder.RegisterModule<ProcessorsAutofacModule>();
            builder.RegisterModule<InterceptorsAutofacModule>();
            builder.RegisterModule(new DataAutofacModule
            {
                Configuration = configuration,
            });
            builder.RegisterModule(new DataAccessAutofacModule
            {
                Configuration = configuration,
            });
            builder.RegisterModule<ServicesWebAutofacModule>();
            builder.RegisterModule(new ServicesAutofacModule
            {
                Configuration = configuration,
            });

            return builder.Build();

            //return container.Resolve<IServiceProvider>();
        }
    }
}
