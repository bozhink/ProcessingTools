// <copyright file="IScopedProcessingService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Scoped processing service.
    /// </summary>
    public interface IScopedProcessingService
    {
        /// <summary>
        /// Start the scoped service.
        /// </summary>
        void Start();

        /// <summary>
        /// Do work.
        /// </summary>
        /// <param name="exceptionHandler">Exception handler.</param>
        void DoWork(Action<Exception> exceptionHandler);

        /// <summary>
        /// Stop the service.
        /// </summary>
        void Stop();
    }
}
