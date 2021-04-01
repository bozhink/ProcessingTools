// <copyright file="ChapterLister.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

// See https://developpaper.com/the-correct-way-to-use-rabbitmq-in-net-core/
namespace ProcessingTools.CommandsServer.Services
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Linq;
    using RabbitMQ.Client;

    /// <summary>
    /// Chapter listener.
    /// </summary>
    public class ChapterLister : RabbitMQListener
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChapterLister"/> class.
        /// </summary>
        /// <param name="serviceProvider">Instance of <see cref="IServiceProvider"/>.</param>
        /// <param name="connectionFactory">Instance of <see cref="IConnectionFactory"/>.</param>
        /// <param name="logger">Instance of <see cref="ILogger"/>.</param>
        public ChapterLister(IServiceProvider serviceProvider, IConnectionFactory connectionFactory, ILogger<ChapterLister> logger)
            : base(connectionFactory, logger)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        protected override string RoutingKey => "done.task";

        /// <inheritdoc/>
        protected override string QueueName => "lemonnovelapi.chapter";

        /// <inheritdoc/>
        protected override string ExchangeName => "message";

        /// <inheritdoc/>
        public override bool Process(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                // Returning to false directly rejects this message, indicating that it cannot be processed.
                return false;
            }

            var taskMessage = JToken.Parse(message);

            if (taskMessage is null)
            {
                // Returning to false directly rejects this message, indicating that it cannot be processed.
                return false;
            }

            try
            {
                using (var scope = this.serviceProvider.CreateScope())
                {
                    ////// TODO: Add processing logic here.
                    ////var xxxService = scope.ServiceProvider.GetRequiredService<XXXXService>();
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Process fail for message: {message}");
                return false;
            }
        }
    }
}
