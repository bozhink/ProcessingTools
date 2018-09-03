// <copyright file="FileExistsRaiseWarningInterceptor.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Ninject.Interceptors
{
    using System;
    using System.IO;
    using global::Ninject.Extensions.Interception;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Logs warning that a given file already exists.
    /// It is supposed that the full file name is passed as first parameter of the call.
    /// </summary>
    public class FileExistsRaiseWarningInterceptor : IInterceptor
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileExistsRaiseWarningInterceptor"/> class.
        /// </summary>
        /// <param name="logger">Logger to log warning for existent file.</param>
        public FileExistsRaiseWarningInterceptor(ILogger<FileExistsRaiseWarningInterceptor> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public void Intercept(IInvocation invocation)
        {
            if (invocation.Request.Arguments.Length < 1)
            {
                throw new InvalidOperationException($"{nameof(FileExistsRaiseWarningInterceptor)} requires invocation with at least 1 argument");
            }

            var fullName = (string)invocation.Request.Arguments[0];
            if (File.Exists(fullName))
            {
                this.logger.LogWarning("Output file '{0}' already exists.", fullName);
            }

            invocation.Proceed();
        }
    }
}
