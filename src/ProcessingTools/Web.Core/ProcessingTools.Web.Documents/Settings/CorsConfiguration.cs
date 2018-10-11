// <copyright file="CorsConfiguration.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// CORS configuration.
    /// </summary>
    public static class CorsConfiguration
    {
        /// <summary>
        /// Configure CORS.
        /// </summary>
        /// <param name="services">Service collection to be configured.</param>
        /// <returns>Configured service collection.</returns>
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins("*");
                });
            });

            return services;
        }

        /// <summary>
        /// Configure CORS.
        /// </summary>
        /// <param name="app">Application builder to be configured.</param>
        /// <returns>Configures application builder.</returns>
        public static IApplicationBuilder ConfigureCors(this IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
            return app;
        }
    }
}
