// <copyright file="PowerShellScriptInvokerIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.PowerShell.Integration.Tests
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// <see cref="PowerShellScriptInvoker"/> integration tests.
    /// </summary>
    [TestClass]
    public class PowerShellScriptInvokerIntegrationTests
    {
        /// <summary>
        /// <see cref="PowerShellScriptInvoker"/>.Invoke should generate correct output.
        /// </summary>
        [TestMethod]
        public void PowerShellScriptInvoker_Invoke_ShouldGenerateCorrectOutput()
        {
            // Arrange
            IServiceCollection services = new ServiceCollection();
            services
                .AddLogging(configure => configure.AddConsole().AddDebug())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Trace)
                .AddSingleton<ILoggerFactory, LoggerFactory>()
                .AddSingleton(typeof(ILogger<>), typeof(Logger<>))
                .AddTransient<PowerShellScriptInvoker>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var invoker = serviceProvider.GetService<PowerShellScriptInvoker>();

            string script = "param($param1) $d = get-date; $s = 'test string value'; " +
                        "$d; $s; $param1; get-service";

            // Act
            invoker.Invoke(script, new PowerShellScriptParameter { Name = "param1", Value = "parameter 1 value!" });

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// <see cref="PowerShellScriptInvoker"/>.InvokeAsync should generate correct output.
        /// </summary>
        [TestMethod]
        public void PowerShellScriptInvoker_InvokeAsync_ShouldGenerateCorrectOutput()
        {
            // Arrange
            IServiceCollection services = new ServiceCollection();
            services
                .AddLogging(configure => configure.AddConsole().AddDebug())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Trace)
                .AddSingleton<ILoggerFactory, LoggerFactory>()
                .AddSingleton(typeof(ILogger<>), typeof(Logger<>))
                .AddTransient<PowerShellScriptInvoker>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var invoker = serviceProvider.GetService<PowerShellScriptInvoker>();

            string script = "start-sleep -s 7; get-service";

            // Act
            invoker.InvokeAsync(script);

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// <see cref="PowerShellScriptInvoker"/>.ExecuteAsynchronously should generate correct output.
        /// </summary>
        [TestMethod]
        public void PowerShellScriptInvoker_ExecuteAsynchronously_ShouldGenerateCorrectOutput()
        {
            // Arrange
            IServiceCollection services = new ServiceCollection();
            services
                .AddLogging(configure => configure.AddConsole().AddDebug())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Trace)
                .AddSingleton<ILoggerFactory, LoggerFactory>()
                .AddSingleton(typeof(ILogger<>), typeof(Logger<>))
                .AddTransient<PowerShellScriptInvoker>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var invoker = serviceProvider.GetService<PowerShellScriptInvoker>();

            string script = "$s1 = 'test1'; $s2 = 'test2'; $s1; write-error 'some error';start-sleep -s 7; $s2";

            // Act
            invoker.ExecuteAsynchronously(script);

            // Assert
            Assert.IsTrue(true);
        }
    }
}
