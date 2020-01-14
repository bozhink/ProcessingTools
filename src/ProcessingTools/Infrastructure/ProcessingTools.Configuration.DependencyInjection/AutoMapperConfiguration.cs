// <copyright file="AutoMapperConfiguration.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.DependencyInjection
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// <see cref="global::AutoMapper"/> configuration.
    /// </summary>
    public static class AutoMapperConfiguration
    {
        /// <summary>
        /// Add configurations of <see cref="global::AutoMapper"/>.
        /// </summary>
        /// <param name="services">Instance of <see cref="IServiceCollection"/> to be configured.</param>
        /// <returns>Updated instance of <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var mapperConfiguration = new global::AutoMapper.MapperConfiguration(c =>
            {
                c.AddMaps(typeof(ProcessingTools.Configuration.AutoMapper.AssemblySetup).Assembly);
            });

            services.AddSingleton<global::AutoMapper.IMapper>(mapperConfiguration.CreateMapper());

            return services;
        }
    }
}
