// <copyright file="Startup.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.TasksServer
{
    using System;
    using Autofac;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.HealthChecks;
    using ProcessingTools.Services;
    using ProcessingTools.TasksServer.Services;

    /// <summary>
    /// Start-up application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">Hosting environment.</param>
        public Startup(IWebHostEnvironment env)
        {
            if (env is null)
            {
                throw new ArgumentNullException(nameof(env));
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();
        }

        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure services.
        /// </summary>
        /// <param name="services">Service collection to be configured.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services
                .AddHealthChecks()
                .AddCheck<VersionHealthCheck>(name: VersionHealthCheck.HealthCheckName);

            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();

            services.AddHostedService<ConsumeScopedServiceHostedService>();
            services.AddHostedService<QueuedHostedService>();
            services.AddHostedService<TimedHostedService>();
        }

        /// <summary>
        /// ConfigureContainer is where you can register things directly
        /// with Autofac. This runs after ConfigureServices so the things
        /// here will override registrations made in ConfigureServices.
        /// Don't build the container; that gets done for you by the factory.
        /// </summary>
        /// <param name="builder">Instance of the <see cref="ContainerBuilder"/>.</param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            // Configure Autofac bindings.
        }

        /// <summary>
        /// Configure is where you add middleware. This is called after
        /// ConfigureContainer. You can use IApplicationBuilder.ApplicationServices
        /// here if you need to resolve things from the container.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="environment">Hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/health", HealthChecksExtensions.GetHealthCheckOptions(this.GetType().Assembly, environment.IsDevelopment()));
            });
        }

        /// <summary>
        /// Configure for the Development environment.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="environment">Hosting environment.</param>
        public void ConfigureDevelopment(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.UseDeveloperExceptionPage();

            this.Configure(app, environment);
        }

        /// <summary>
        /// Configure for the Staging environment.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="environment">Hosting environment.</param>
        public void ConfigureStaging(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

            this.Configure(app, environment);
        }

        /// <summary>
        /// Configure for the Production environment.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="environment">Hosting environment.</param>
        public void ConfigureProduction(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            this.ConfigureStaging(app, environment);
        }
    }
}
