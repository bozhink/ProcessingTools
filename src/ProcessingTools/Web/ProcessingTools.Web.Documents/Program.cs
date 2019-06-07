// <copyright file="Program.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents
{
    using System;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
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
                logger.Debug("Start application");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                // NLog: catch setup errors
                logger.Fatal(ex, "Stopped program because of exception");
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid
                // segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseEnvironment("Development")
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .UseNLog() // NLog: setup NLog for Dependency injection
                ;
    }
}
