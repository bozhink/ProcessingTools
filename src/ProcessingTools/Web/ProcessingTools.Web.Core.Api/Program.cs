// <copyright file="Program.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
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

    /// <summary>
    /// Entry point of the application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Entry point method of the application.
        /// </summary>
        /// <param name="args">Arguments to run the application.</param>
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
                        .ConfigureLogging((hostingContext, builder) =>
                        {
                            builder.ClearProviders();
                            builder.SetMinimumLevel(LogLevel.Trace);
                            builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                            builder.AddConsole();
                            builder.AddDebug();
                        })
                        .UseNLog() // NLog: setup NLog for Dependency injection
                        ;
                });
    }
}
