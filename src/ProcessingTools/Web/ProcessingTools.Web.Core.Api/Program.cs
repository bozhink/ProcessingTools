// <copyright file="Program.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Core.Api
{
    using System;
    using System.Reflection;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using NLog.Web;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Configuration.Extensions;

    /// <summary>
    /// Entry point of the application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Entry point method of the application.
        /// </summary>
        /// <param name="args">Arguments to run the application.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Startup fatal exception")]
        public static void Main(string[] args)
        {
            // NLog: setup the logger first to catch all errors
            var logger = NLog.LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug($"Start application {Assembly.GetExecutingAssembly().Location}");

                var hostBuilder = CreateHostBuilder(args);

                var host = hostBuilder.Build();

                host.Run();
            }
            catch (Exception ex)
            {
                // NLog: catch setup errors
                logger.Fatal(ex, "Stopped program because of exception");
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder
                        .UseStartup<Startup>()
                        .UseKestrel(options => options.ConfigureEndpoints().RequireCertificate())
                        .ConfigureLogging((hostingContext, builder) =>
                        {
                            builder.ClearProviders();
                            builder.SetMinimumLevel(hostingContext.HostingEnvironment.IsDevelopment() ? LogLevel.Trace : LogLevel.Debug);
                            builder.AddConfiguration(hostingContext.Configuration.GetSection(ConfigurationConstants.LoggingSectionName));
                            builder.AddConsole();
                            builder.AddDebug();
                        })
                        .UseNLog() // NLog: setup NLog for Dependency injection
                        ;
                });
    }
}
