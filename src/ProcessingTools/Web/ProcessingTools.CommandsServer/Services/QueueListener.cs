// <copyright file="QueueListener.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.CommandsServer.Services
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Cache;
    using ProcessingTools.Contracts.Services.MQ;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using RabbitMQ.Client.Exceptions;

    /// <summary>
    /// Queue listener.
    /// </summary>
    public class QueueListener : IQueueListener
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly IQueueConfiguration queueConfiguration;
        private readonly IMessageCacheService messageCacheService;
        private readonly ILogger logger;
        private IConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueListener"/> class.
        /// </summary>
        /// <param name="connectionFactory">Instance of <see cref="IConnectionFactory"/>.</param>
        /// <param name="queueConfiguration">Configuration of the message queue.</param>
        /// <param name="messageCacheService">Instance of <see cref="IMessageCacheService"/>.</param>
        /// <param name="logger">Logger.</param>
        public QueueListener(IConnectionFactory connectionFactory, IQueueConfiguration queueConfiguration, IMessageCacheService messageCacheService, ILogger<QueueListener> logger)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            this.queueConfiguration = queueConfiguration ?? throw new ArgumentNullException(nameof(queueConfiguration));
            this.messageCacheService = messageCacheService ?? throw new ArgumentNullException(nameof(messageCacheService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public void Start(Action<Exception> exceptionHandler)
        {
            if (exceptionHandler is null)
            {
                throw new ArgumentNullException(nameof(exceptionHandler));
            }

            this.connection = this.connectionFactory.CreateConnection();
            this.ConfigureConnection(this.connection, exceptionHandler);
        }

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Handled exception")]
        public void Run(Action<Exception> exceptionHandler)
        {
            if (exceptionHandler is null)
            {
                throw new ArgumentNullException(nameof(exceptionHandler));
            }

            if (this.connection is null)
            {
                throw new InvalidOperationException("Queue Listener must be started first.");
            }

            try
            {
                IModel channel = this.connection.CreateModel();
                this.ConfigureModel(channel, exceptionHandler);
            }
            catch (Exception ex)
            {
                exceptionHandler.Invoke(ex);
            }
        }

        /// <inheritdoc/>
        public void Stop(Action<Exception> exceptionHandler)
        {
            if (exceptionHandler is null)
            {
                throw new ArgumentNullException(nameof(exceptionHandler));
            }

            if (this.connection != null)
            {
                this.connection.Abort();
                this.connection.Dispose();
            }
        }

        private void ConfigureConnection(IConnection connection, Action<Exception> exceptionHandler)
        {
            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            if (exceptionHandler is null)
            {
                throw new ArgumentNullException(nameof(exceptionHandler));
            }

            connection.CallbackException += (s, e) =>
            {
                var exception = e.Exception;

                if (exception is AggregateException aggregateException && aggregateException.InnerExceptions.Any(ex => ex is ConnectFailureException))
                {
                    this.messageCacheService.Message = "No connection to the RabbitMQ server";
                    this.messageCacheService.Exception = exception;
                }
                else
                {
                    this.messageCacheService.Message = "No connection to the RabbitMQ server";
                }

                this.logger.LogError(e.Exception, "IConnection.CallbackException");
            };

            connection.ConnectionShutdown += (s, e) =>
            {
                this.messageCacheService.Message = "Shutdown connection to the RabbitMQ";
                exceptionHandler.Invoke(new Exception(this.messageCacheService.Message));
                this.logger.LogInformation("IConnection.ConnectionShutdown {0}", e.ReplyText);
            };

            connection.ConnectionBlocked += (s, e) =>
            {
                this.logger.LogWarning("IConnection.ConnectionBlocked {0}", e.Reason);
            };

            connection.ConnectionUnblocked += (s, e) =>
            {
                this.messageCacheService.Message = "OK";
                this.logger.LogWarning("IConnection.ConnectionUnblocked {0}", e.ToString());
            };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Handled exception")]
        private void ConfigureModel(IModel channel, Action<Exception> exceptionHandler)
        {
            channel.CallbackException += (s, e) =>
            {
                this.messageCacheService.Message = e.Exception.Message;
                this.messageCacheService.Exception = e.Exception;
                this.logger.LogError(e.Exception, "IModel.CallbackException");
            };

            channel.QueueDeclare(queue: this.queueConfiguration.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                try
                {
                    byte[] body = ea.Body.ToArray();
                    string message = Defaults.Encoding.GetString(body);

                    // TODO: Add processing logic here.
                    this.messageCacheService.Message = "OK";
                    this.logger.LogDebug(message);
                }
                catch (Exception ex)
                {
                    exceptionHandler.Invoke(ex);
                }
            };

            channel.BasicConsume(queue: this.queueConfiguration.QueueName, autoAck: false, consumer: consumer);
        }
    }
}
