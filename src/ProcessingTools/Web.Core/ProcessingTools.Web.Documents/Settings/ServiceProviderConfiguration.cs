// <copyright file="ServiceProviderConfiguration.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using System;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ProcessingTools.Contracts;
    using ProcessingTools.Web.Models.Shared;
    using ProcessingTools.Web.Services;
    using ProcessingTools.Web.Services.Contracts;

    /// <summary>
    /// Service provider configuration.
    /// </summary>
    public static class ServiceProviderConfiguration
    {
        /// <summary>
        /// Build service provider.
        /// </summary>
        /// <param name="services">Service collection to be configured.</param>
        /// <param name="configuration">Application configuration.</param>
        /// <returns>Configured service collection.</returns>
        public static IServiceProvider BuildServiceProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            // Add bindings
            builder.Populate(services);

            builder.RegisterType<ApplicationContextFactory>().AsSelf().InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<ApplicationContextFactory>().ApplicationContext).As<IApplicationContext>().InstancePerDependency();
            builder.Register(c => c.Resolve<IApplicationContext>().UserContext).As<IUserContext>().InstancePerDependency();

            builder.RegisterType<ProcessingTools.Harvesters.Meta.JatsArticleMetaHarvester>().As<ProcessingTools.Harvesters.Contracts.Meta.IJatsArticleMetaHarvester>().InstancePerDependency();
            builder.RegisterType<ProcessingTools.Services.IO.XmlReadService>().As<ProcessingTools.Services.Contracts.IO.IXmlReadService>().InstancePerDependency();

            builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerDependency();

            builder.RegisterInstance(ProcessingTools.Common.Constants.Defaults.Encoding).As<System.Text.Encoding>().SingleInstance();

            builder.RegisterModule(new XmlTransformersAutofacModule
            {
                Configuration = configuration
            });
            builder.RegisterModule<TransformersFactoriesAutofacModule>();
            builder.RegisterModule<ProcessorsAutofacModule>();
            builder.RegisterModule<InterceptorsAutofacModule>();
            builder.RegisterModule(new DataAutofacModule
            {
                Configuration = configuration
            });
            builder.RegisterModule<ServicesWebAutofacModule>();
            builder.RegisterModule<ServicesAutofacModule>();

            var container = builder.Build();

            return container.Resolve<IServiceProvider>();
        }
    }
}
