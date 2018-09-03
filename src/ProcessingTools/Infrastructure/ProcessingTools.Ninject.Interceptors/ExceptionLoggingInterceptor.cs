// <copyright file="ExceptionLoggingInterceptor.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

// See https://blog.kulman.sk/intercepting-methods-with-ninject-for-error-logging/
namespace ProcessingTools.Ninject.Interceptors
{
    using System;
    using System.Text;
    using global::Ninject.Extensions.Interception;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Exception logging interceptor.
    /// </summary>
    public class ExceptionLoggingInterceptor : IInterceptor
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionLoggingInterceptor"/> class.
        /// </summary>
        /// <param name="logger">Logger to log caught exception.</param>
        public ExceptionLoggingInterceptor(ILogger<ExceptionLoggingInterceptor> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                var sb = new StringBuilder();
                sb.Append($"Executing {invocation.Request.Target.GetType().Name}.{invocation.Request.Method.Name} (");

                var parameters = invocation.Request.Method.GetParameters();
                for (int i = 0; i < invocation.Request.Arguments.Length; ++i)
                {
                    sb.Append($"{parameters[i].Name}={invocation.Request.Arguments[i]},");
                }

                sb.Append($") {e.GetType().Name} caught: {e.Message})");

                this.logger.LogError(e, sb.ToString());
                throw;
            }
        }
    }
}
