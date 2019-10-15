// <copyright file="Startup.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.CommandsServer
{
    using System;
    using Autofac;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ProcessingTools.CommandsServer.Services;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Configuration.Models;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Cache;
    using ProcessingTools.Contracts.Services.MQ;
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
        public void Configure(IApplicationBuilder app)
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
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        /// <summary>
        /// Configure for the Development environment.
        /// </summary>
        /// <param name="app">Application builder.</param>
        public void ConfigureDevelopment(IApplicationBuilder app)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.UseDeveloperExceptionPage();

            this.Configure(app);
        }

        /// <summary>
        /// Configure for the Staging environment.
        /// </summary>
        /// <param name="app">Application builder.</param>
        public void ConfigureStaging(IApplicationBuilder app)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

            this.Configure(app);
        }

        /// <summary>
        /// Configure for the Production environment.
        /// </summary>
        /// <param name="app">Application builder.</param>
        public void ConfigureProduction(IApplicationBuilder app)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            this.ConfigureStaging(app);
        }
    }
}
