// <copyright file="IQueueListener.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System;

namespace ProcessingTools.Contracts.Services.MQ
{
    /// <summary>
    /// Queue listener.
    /// </summary>
    public interface IQueueListener
    {
        /// <summary>
        /// Start the queue listener.
        /// </summary>
        void Start();

        /// <summary>
        /// Run the queue listener.
        /// </summary>
        /// <param name="exceptionHandler">Error handler.</param>
        void Run(Action<Exception> exceptionHandler);

        /// <summary>
        /// Stop the queue listener.
        /// </summary>
        void Stop();
    }
}
