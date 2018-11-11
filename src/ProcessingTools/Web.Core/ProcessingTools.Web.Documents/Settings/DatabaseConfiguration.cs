// <copyright file="DatabaseConfiguration.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Web.Documents.Data;

    /// <summary>
    /// Database configuration.
    /// </summary>
    public static class DatabaseConfiguration
    {
        /// <summary>
        /// Configure databases.
        /// </summary>
        /// <param name="services">Service collection to be configured.</param>
        /// <param name="configuration">Application configuration.</param>
        /// <returns>Configured service collection.</returns>
        public static IServiceCollection ConfigureDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString(ConfigurationConstants.DefaultConnectionConnectionStringName));
            });

            return services;
        }
    }
}
