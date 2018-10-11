// <copyright file="MvcCoreConfiguration.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// MVC Core configuration.
    /// </summary>
    public static class MvcCoreConfiguration
    {
        /// <summary>
        /// Configure MVC Core.
        /// </summary>
        /// <param name="services">Service collection to be configured.</param>
        /// <returns>Configured service collection.</returns>
        public static IServiceCollection ConfigureMvcCore(this IServiceCollection services)
        {
            IMvcCoreBuilder mvcCoreBuilder = services.AddMvcCore();
            
            mvcCoreBuilder
                .AddApiExplorer()
                .AddAuthorization()
                .AddFormatterMappings()
                .AddJsonFormatters()
                .AddXmlDataContractSerializerFormatters()
                .AddXmlSerializerFormatters()
                .AddViews()
                .AddRazorViewEngine()
                .AddCacheTagHelper()
                .AddDataAnnotations()
                .AddCors();

            return services;
        }
    }
}
