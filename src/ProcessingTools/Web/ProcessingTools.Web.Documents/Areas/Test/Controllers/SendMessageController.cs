// <copyright file="SendMessageController.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Test.Controllers
{
    using System;
    using System.Text;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using ProcessingTools.Web.Documents.Constants;
    using RabbitMQ.Client;

    /// <summary>
    /// Send message to the queue.
    /// </summary>
    [Authorize]
    [Area(AreaNames.Test)]
    public class SendMessageController : Controller
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessageController"/> class.
        /// </summary>
        /// <param name="connectionFactory">Instance of <see cref="IConnectionFactory"/>.</param>
        /// <param name="logger">Logger.</param>
        public SendMessageController(IConnectionFactory connectionFactory, ILogger<SendMessageController> logger)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Index page.
        /// </summary>
        /// <returns>Index page view.</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Send message page.
        /// </summary>
        /// <returns>Send message page view.</returns>
        public IActionResult Send()
        {
            return this.View();
        }

        /// <summary>
        /// Send message.
        /// </summary>
        /// <param name="message">Message to be send.</param>
        /// <returns>Submission status.</returns>
        [HttpPost]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public IActionResult Send(string message)
        {
            try
            {
                using (var connection = this.connectionFactory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(
                            queue: "lemonnovelapi.chapter",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                        string json = JsonConvert.SerializeObject(message);
                        var body = Encoding.UTF8.GetBytes(json);

                        channel.BasicPublish(
                            exchange: "message",
                            routingKey: "done.task",
                            basicProperties: null,
                            body: body);
                    }
                }

                this.ViewData[ContextKeys.Success] = true;
                return this.View(model: message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, string.Empty);
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.ViewData[ContextKeys.Success] = false;
                return this.View(model: message);
            }
        }
    }
}
