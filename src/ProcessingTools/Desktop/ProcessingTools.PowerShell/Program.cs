// <copyright file="Program.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.PowerShell
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Main program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">Arguments.</param>
        public static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            if (args != null && args.Length > 0)
            {
                string script = args[0];

                IList<IList<string>> parameterPairs = new List<IList<string>>();
                for (int i = 1; i < args.Length; i++)
                {
                    var pair = args[i].Trim(new[] { ' ', '-', '/' }).Split('=', StringSplitOptions.RemoveEmptyEntries);
                    if (pair.Length == 2)
                    {
                        parameterPairs.Add(pair);
                    }
                }

                var parameters = parameterPairs.Select(p => new PowerShellScriptParameter { Name = p[0], Value = p[1] }).ToArray();

                var invoker = serviceProvider.GetService<PowerShellScriptInvoker>();

                invoker.Invoke(script, parameters);
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddLogging(configure => configure.AddConsole().AddDebug())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Trace)
                .AddSingleton<ILoggerFactory, LoggerFactory>()
                .AddSingleton(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}
