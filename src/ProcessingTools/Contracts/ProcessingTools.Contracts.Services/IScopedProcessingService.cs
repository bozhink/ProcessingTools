// <copyright file="IScopedProcessingService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using System;

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
