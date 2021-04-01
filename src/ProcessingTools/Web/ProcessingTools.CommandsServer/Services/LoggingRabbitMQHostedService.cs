// <copyright file="LoggingRabbitMQHostedService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.CommandsServer.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    /// <summary>
    /// Logging RabbitMQ Hosted Service.
    /// </summary>
    public class LoggingRabbitMQHostedService : BackgroundService
    {
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingRabbitMQHostedService"/> class.
        /// </summary>
        /// <param name="connectionFactory">Instance of <see cref="IConnectionFactory"/>.</param>
        /// <param name="logger">Logger.</param>
        public LoggingRabbitMQHostedService(IConnectionFactory connectionFactory, ILogger<LoggingRabbitMQHostedService> logger)
        {
            if (connectionFactory is null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            this.connection = connectionFactory.CreateConnection();
            this.connection.ConnectionShutdown += this.Connection_ConnectionShutdown;

            this.channel = this.connection.CreateModel();
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            this.channel.Close();
            this.connection.Close();
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            this.channel.ExchangeDeclare("demo.exchange", ExchangeType.Topic);
            this.channel.QueueDeclare("demo.queue.log", false, false, false, null);
            this.channel.QueueBind("demo.queue.log", "demo.exchange", "demo.queue.*", null);
            this.channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(this.channel);

            consumer.Received += (ch, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var content = System.Text.Encoding.UTF8.GetString(body);
                this.HandleMessage(content);
                this.channel.BasicAck(ea.DeliveryTag, false);
            };

            consumer.Shutdown += this.Consumer_Shutdown;
            consumer.Registered += this.Consumer_Registered;
            consumer.Unregistered += this.Consumer_Unregistered;
            consumer.ConsumerCancelled += this.Consumer_ConsumerCancelled;

            this.channel.BasicConsume("demo.queue.log", false, consumer);

            return Task.CompletedTask;
        }

        private void HandleMessage(string content)
        {
            this.logger.LogInformation($"consumer received {content}");
        }

        private void Consumer_ConsumerCancelled(object sender, ConsumerEventArgs e)
        {
            // Method intentionally left empty.
        }

        private void Consumer_Unregistered(object sender, ConsumerEventArgs e)
        {
            // Method intentionally left empty.
        }

        private void Consumer_Registered(object sender, ConsumerEventArgs e)
        {
            // Method intentionally left empty.
        }

        private void Consumer_Shutdown(object sender, ShutdownEventArgs e)
        {
            // Method intentionally left empty.
        }

        private void Connection_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            // Method intentionally left empty.
        }
    }
}
