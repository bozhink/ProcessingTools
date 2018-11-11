// <copyright file="CommandRunnerTimeLoggingInterceptor.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Ninject.Interceptors
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using global::Ninject.Extensions.Interception;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Processors.Contracts;

    /// <summary>
    /// Command runner time logging interceptor.
    /// </summary>
    public class CommandRunnerTimeLoggingInterceptor : IInterceptor
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandRunnerTimeLoggingInterceptor"/> class.
        /// </summary>
        /// <param name="logger">Logger to log timings and exceptions.</param>
        public CommandRunnerTimeLoggingInterceptor(ILogger<CommandRunnerTimeLoggingInterceptor> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public void Intercept(IInvocation invocation)
        {
            if (invocation.Request.Target is ICommandRunner && invocation.Request.Method.Name == nameof(ICommandRunner.RunAsync))
            {
                var target = invocation.Request.Target as ICommandRunner;
                var commandName = invocation.Request.Arguments.Single().ToString();

                var timer = new Stopwatch();
                timer.Start();

                var invocationMessage = $"{invocation.Request.Target.GetType().Name}.{invocation.Request.Method.Name}({commandName})";

                try
                {
                    invocation.ReturnValue = Task.FromResult(target.RunAsync(commandName).Result);
                }
                catch (AggregateException e)
                {
                    foreach (var i in e.InnerExceptions)
                    {
                        this.logger.LogError(i, "\nInvocation: {0}\n", invocationMessage);
                    }

                    invocation.ReturnValue = Task.FromResult<object>(false);
                }
                catch (Exception e)
                {
                    this.logger.LogError(e, "\nInvocation: {0}\n", invocationMessage);
                    invocation.ReturnValue = Task.FromResult<object>(false);
                }

                this.logger.LogDebug("Elapsed time for execution of {0}: {1}.", invocationMessage, timer.Elapsed);
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}
