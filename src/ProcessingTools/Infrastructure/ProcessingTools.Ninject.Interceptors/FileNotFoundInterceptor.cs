// <copyright file="FileNotFoundInterceptor.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Ninject.Interceptors
{
    using System;
    using System.IO;
    using global::Ninject.Extensions.Interception;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// File-Not-Found interceptor.
    /// </summary>
    public class FileNotFoundInterceptor : IInterceptor
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileNotFoundInterceptor"/> class.
        /// </summary>
        /// <param name="logger">Logger to trace thrown exception.</param>
        public FileNotFoundInterceptor(ILogger<FileNotFoundInterceptor> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public void Intercept(IInvocation invocation)
        {
            if (invocation.Request.Arguments.Length < 1)
            {
                throw new InvalidOperationException($"{nameof(FileNotFoundInterceptor)} requires invocation with at least 1 argument");
            }

            var fileName = (string)invocation.Request.Arguments[0];
            if (!File.Exists(fileName))
            {
                string message = $"File '{fileName}' does not exist.";
                this.logger.LogError(message);
                throw new FileNotFoundException(message: message, fileName: fileName);
            }

            invocation.Proceed();
        }
    }
}
