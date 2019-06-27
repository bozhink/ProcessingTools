// <copyright file="Startup.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.CommandsServer
{
    using System;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ProcessingTools.CommandsServer.Services;
    using ProcessingTools.Services.Contracts.MQ;
    using ProcessingTools.Services.MQ;

    /// <summary>
    /// Start-up application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure services.
        /// </summary>
        /// <param name="services">Service collection to be configured.</param>
        /// <returns>Configured service provider.</returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IQueueListener, QueueListener>();
            services.AddScoped<IQueueListenerScopedProcessingService, QueueListenerScopedProcessingService>();
            services.AddHostedService<ConsumeScopedServiceHostedService<IQueueListenerScopedProcessingService>>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var builder = new ContainerBuilder();
            builder.Populate(services);

            var container = builder.Build();

            return container.Resolve<IServiceProvider>();
        }

        /// <summary>
        /// Configure application.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Application environment.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
