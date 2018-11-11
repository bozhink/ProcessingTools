// <copyright file="CorsConfiguration.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Authorization configuration.
    /// </summary>
    public static class AuthorizationConfiguration
    {
        /// <summary>
        /// Configure authorization.
        /// </summary>
        /// <param name="services">Service collection to be configured.</param>
        /// <returns>Configured service collection.</returns>
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
        {
            // See https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims
            // See https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ElevatedRights", policy =>
                {
                    policy.RequireRole("Administrator", "PowerUser", "BackupAdministrator");
                });
            });

            return services;
        }
    }
}
