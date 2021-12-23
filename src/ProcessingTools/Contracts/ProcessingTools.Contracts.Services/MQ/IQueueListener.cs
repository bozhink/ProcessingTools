// <copyright file="IQueueListener.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.MQ
{
    using System;

    /// <summary>
    /// Queue listener.
    /// </summary>
    public interface IQueueListener
    {
        /// <summary>
        /// Starts the queue listener.
        /// </summary>
        /// <param name="exceptionHandler">Exception handler.</param>
        void Start(Action<Exception> exceptionHandler);

        /// <summary>
        /// Runs the queue listener.
        /// </summary>
        /// <param name="exceptionHandler">Exception handler.</param>
        void Run(Action<Exception> exceptionHandler);

        /// <summary>
        /// Stops the queue listener.
        /// </summary>
        /// <param name="exceptionHandler">Exception handler.</param>
        void Stop(Action<Exception> exceptionHandler);
    }
}
