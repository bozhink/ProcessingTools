// <copyright file="IScopedProcessingService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
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
        /// Starts the scoped processing service.
        /// </summary>
        /// <param name="exceptionHandler">Exception handler.</param>
        void Start(Action<Exception> exceptionHandler);

        /// <summary>
        /// Runs the main work logic of the scoped processing service.
        /// </summary>
        /// <param name="exceptionHandler">Exception handler.</param>
        void DoWork(Action<Exception> exceptionHandler);

        /// <summary>
        /// Stops the scoped processing service.
        /// </summary>
        /// <param name="exceptionHandler">Exception handler.</param>
        void Stop(Action<Exception> exceptionHandler);
    }
}
