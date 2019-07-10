// <copyright file="RabbitMQConnectionExtensions.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Cache;

namespace ProcessingTools.CommandsServer.Extensions
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.Logging;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Exceptions;

    /// <summary>
    /// RabbitMQ connection extensions.
    /// </summary>
    public static class RabbitMQConnectionExtensions
    {
        /// <summary>
        /// Configure connection.
        /// </summary>
        /// <param name="connection">Instance of <see cref="IConnection"/> to be configured.</param>
        /// <param name="messageCacheService">Instance of <see cref="IMessageCacheService"/>.</param>
        /// <param name="logger">Logger.</param>
        /// <returns>Configured connection.</returns>
        public static IConnection ConfigureConnection(this IConnection connection, IMessageCacheService messageCacheService, ILogger logger)
        {
            connection.CallbackException += (s, e) =>
            {
                logger.LogError(e.Exception, "IConnection.CallbackException");
            };

            connection.RecoverySucceeded += (s, e) =>
            {
                messageCacheService.Message = "OK";

                logger.LogInformation("IConnection.RecoverySucceeded {0}", e.ToString());
            };

            connection.ConnectionRecoveryError += (s, e) =>
            {
                var exception = e.Exception;

                if (exception is AggregateException aggregateException && aggregateException.InnerExceptions.Any(ex => ex is ConnectFailureException))
                {
                    messageCacheService.Message = "No connection to the RabbitMQ server";
                }

                logger.LogError(e.Exception, "IConnection.ConnectionRecoveryError");
            };

            connection.ConnectionShutdown += (s, e) =>
            {
                logger.LogInformation("IConnection.ConnectionShutdown {0}", e.ReplyText);
            };

            connection.ConnectionBlocked += (s, e) =>
            {
                logger.LogWarning("IConnection.ConnectionBlocked {0}", e.Reason);
            };

            connection.ConnectionUnblocked += (s, e) =>
            {
                logger.LogWarning("IConnection.ConnectionUnblocked {0}", e.ToString());
            };

            return connection;
        }
    }
}
