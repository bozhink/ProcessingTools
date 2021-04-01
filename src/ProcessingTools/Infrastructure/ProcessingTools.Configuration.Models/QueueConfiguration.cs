// <copyright file="QueueConfiguration.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.Models
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Queue configuration.
    /// </summary>
    public class QueueConfiguration : IQueueConfiguration
    {
        /// <summary>
        /// Gets or sets the queue name.
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// Gets or sets the exchange name.
        /// </summary>
        public string ExchangeName { get; set; }
    }
}
