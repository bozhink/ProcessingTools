// <copyright file="Startup.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.CommandsServer
{
    using System;
    using System.Linq;
    using Autofac;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using ProcessingTools.CommandsServer.Services;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Configuration.Models;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Cache;
    using ProcessingTools.Contracts.Services.MQ;
    using ProcessingTools.HealthChecks;
    using ProcessingTools.Services.Cache;
    using ProcessingTools.Services.MQ;
    using RabbitMQ.Client;

    /// <summary>
    /// Start-up application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="environment">Hosting environment.</param>
        public Startup(IWebHostEnvironment environment)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
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
                .AddCheck<VersionHealthCheck>("version")
                .AddCheck<QueueHealthCheck>("queue");

            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddScoped<IQueueListener, QueueListener>();
            services.AddScoped<IQueueListenerScopedProcessingService, QueueListenerScopedProcessingService>();
            services.AddHostedService<ConsumeScopedServiceHostedService<IQueueListenerScopedProcessingService>>();
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
            builder.RegisterType<MessageCacheService>().As<IMessageCacheService>().SingleInstance();

            // Rabbit MQ
            builder
                .Register(c => new ConnectionFactory
                {
                    HostName = this.Configuration.GetValue<string>(ConfigurationConstants.MessageQueueHostName),
                    Port = this.Configuration.GetValue<int>(ConfigurationConstants.MessageQueuePort),
                    VirtualHost = this.Configuration.GetValue<string>(ConfigurationConstants.MessageQueueVirtualHost),
                    UserName = this.Configuration.GetValue<string>(ConfigurationConstants.MessageQueueUserName),
                    Password = this.Configuration.GetValue<string>(ConfigurationConstants.MessageQueuePassword),
                    AutomaticRecoveryEnabled = true,
                    Ssl = new SslOption
                    {
                        AcceptablePolicyErrors = System.Net.Security.SslPolicyErrors.None,
                    },
                })
                .As<IConnectionFactory>()
                .SingleInstance();

            builder
                .RegisterInstance(new QueueConfiguration
                {
                    QueueName = this.Configuration.GetValue<string>(ConfigurationConstants.MessageQueueQueueName),
                    ExchangeName = this.Configuration.GetValue<string>(ConfigurationConstants.MessageQueueExchangeName),
                })
                .As<IQueueConfiguration>()
                .SingleInstance();
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

                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    AllowCachingResponses = false,
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                    },
                    ResponseWriter = (HttpContext httpContext, HealthReport result) =>
                    {
                        httpContext.Response.ContentType = "application/json";

                        var json = new JObject
                        {
                            new JProperty("version", this.GetType().Assembly.GetName().Version?.ToString()),
                            new JProperty("status", result.Status.ToString()),
                        };

                        if (result.Entries.Any())
                        {
                            json.Add(result.GetResultsToJSON(environment.EnvironmentName == "Development"));
                        }

                        return httpContext.Response.WriteAsync(json.ToString(Formatting.None));
                    },
                });
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
