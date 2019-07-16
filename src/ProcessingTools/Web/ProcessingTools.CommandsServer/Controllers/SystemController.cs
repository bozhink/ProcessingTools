// <copyright file="SystemController.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.CommandsServer.Controllers
{
    using System;
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Web.Models.Application;
    using RabbitMQ.Client;

    /// <summary>
    /// System controller.
    /// </summary>
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly IConnection connection;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemController"/> class.
        /// </summary>
        /// <param name="connection">Instance of <see cref="IConnection"/>.</param>
        /// <param name="logger">Logger.</param>
        public SystemController(IConnection connection, ILogger<SystemController> logger)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Checks the system status.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        public IActionResult Check()
        {
            try
            {
                if (!this.connection.IsOpen)
                {
                    return this.StatusCode((int)HttpStatusCode.ServiceUnavailable, new CheckSystemResponseModel
                    {
                        IsOK = false,
                        Message = $"RabbitMQ connection is closed: {this.connection.CloseReason}",
                    });
                }

                return this.Ok(new CheckSystemResponseModel
                {
                    IsOK = true,
                });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Check system error: {ex.Message}");
                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
