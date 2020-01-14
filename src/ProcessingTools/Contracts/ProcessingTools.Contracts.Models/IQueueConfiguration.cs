// <copyright file="IQueueConfiguration.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Queue configuration.
    /// </summary>
    public interface IQueueConfiguration
    {
        /// <summary>
        /// Gets the queue name.
        /// </summary>
        string QueueName { get; }

        /// <summary>
        /// Gets the exchange name.
        /// </summary>
        string ExchangeName { get; }
    }
}
