// <copyright file="MvcConfiguration.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json.Serialization;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Documents.Controllers;
    using ProcessingTools.Web.Documents.Formatters;

    /// <summary>
    /// MVC configuration.
    /// </summary>
    public static class MvcConfiguration
    {
        /// <summary>
        /// Configure MVC.
        /// </summary>
        /// <param name="services">Service collection to be configured.</param>
        /// <returns>Configured service collection.</returns>
        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            IMvcBuilder mvcBuilder = services.AddMvc(options =>
            {
                options.InputFormatters.Insert(0, new RawRequestBodyFormatter());
                options.MaxModelValidationErrors = 50;
            });

            mvcBuilder
                .AddJsonOptions(o => o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                .AddXmlDataContractSerializerFormatters()
                .AddXmlSerializerFormatters()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            return services;
        }

        /// <summary>
        /// Configure MVC.
        /// </summary>
        /// <param name="app">Application builder to be configured.</param>
        /// <returns>Configures application builder.</returns>
        public static IApplicationBuilder ConfigureMvc(this IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "areaUnknownActionRoute",
                    template: "{area:exists}/{controller}/{*params}",
                    defaults: new { area = AreaNames.Default, controller = ErrorController.ControllerName, action = ErrorController.HandleUnknownActionActionName });

                routes.MapRoute(
                    name: "defaultRoute",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "defaultUnknownActionRoute",
                    template: "{controller}/{*params}",
                    defaults: new { area = AreaNames.Default, controller = ErrorController.ControllerName, action = ErrorController.HandleUnknownActionActionName });
            });

            return app;
        }
    }
}
