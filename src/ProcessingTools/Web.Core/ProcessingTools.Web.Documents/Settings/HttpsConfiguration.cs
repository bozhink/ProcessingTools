// <copyright file="HttpsConfiguration.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// HTTPS configuration.
    /// </summary>
    public static class HttpsConfiguration
    {
        /// <summary>
        /// Configure HTTPS.
        /// </summary>
        /// <param name="services">Service collection to be configured.</param>
        /// <returns>Configured service collection.</returns>
        public static IServiceCollection ConfigureHttps(this IServiceCollection services)
        {
            // See https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-2.1&tabs=visual-studio
            //// services.AddHsts(options =>
            //// {
            ////     options.Preload = true;
            ////     options.IncludeSubDomains = true;
            ////     options.MaxAge = TimeSpan.FromDays(30);
            //// });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 24173;
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            return services;
        }
    }
}
