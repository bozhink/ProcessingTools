// <copyright file="RabbitMQListener.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

// See https://developpaper.com/the-correct-way-to-use-rabbitmq-in-net-core/
namespace ProcessingTools.CommandsServer.Services
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    /// <summary>
    /// RabbitMQListener is the base class. It only implements registering RabbitMQ to listen for messages,
    /// and then each consumer rewrites RouteKey/QueueName/Message Processing Function Process itself.
    /// </summary>
    public abstract class RabbitMQListener : IHostedService, IDisposable
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly ILogger logger;

        private IConnection connection;
        private IModel channel;

        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMQListener"/> class.
        /// </summary>
        /// <param name="connectionFactory">Instance of <see cref="IConnectionFactory"/>.</param>
        /// <param name="logger">Instance of <see cref="ILogger"/>.</param>
        protected RabbitMQListener(IConnectionFactory connectionFactory, ILogger logger)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="RabbitMQListener"/> class.
        /// </summary>
        ~RabbitMQListener()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the routing key.
        /// </summary>
        protected abstract string RoutingKey { get; }

        /// <summary>
        /// Gets the queue name.
        /// </summary>
        protected abstract string QueueName { get; }

        /// <summary>
        /// Gets the exchange name.
        /// </summary>
        protected abstract string ExchangeName { get; }

        /// <inheritdoc/>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.Register();
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.DeRegister();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Process a message.
        /// </summary>
        /// <param name="message">Message to be processed.</param>
        /// <returns>Procession status.</returns>
        public abstract bool Process(string message);

        /// <summary>
        /// Register consumer monitor.
        /// </summary>
        public void Register()
        {
            this.logger.LogInformation($"RabbitListener register, routeKey: {this.RoutingKey}");

            this.connection = this.connectionFactory.CreateConnection();
            this.channel = this.connection.CreateModel();

            this.channel.ExchangeDeclare(exchange: this.ExchangeName, type: "topic");
            this.channel.QueueDeclare(queue: this.QueueName, exclusive: false);
            this.channel.QueueBind(queue: this.QueueName, exchange: this.ExchangeName, routingKey: this.RoutingKey);

            var consumer = new EventingBasicConsumer(this.channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                var result = this.Process(message);
                if (result)
                {
                    this.channel.BasicAck(ea.DeliveryTag, false);
                }
            };

            this.channel.BasicConsume(queue: this.QueueName, consumer: consumer);
        }

        /// <summary>
        /// De-register consumer monitor.
        /// </summary>
        public void DeRegister()
        {
            this.connection.Close();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            this.Dispose(true);

            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispose pattern.
        /// </summary>
        /// <param name="disposing">Disposing parameter.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                /*
                 * Free any other managed objects here.
                 */

                this.channel?.Dispose();
                this.connection?.Dispose();
            }

            /*
             * Free any unmanaged objects here.
             */

            this.disposed = true;
        }
    }
}
