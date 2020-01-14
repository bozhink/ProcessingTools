// <copyright file="LoggingHandler.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Core.Api.Handlers
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Logging handler.
    /// </summary>
    public class LoggingHandler : DelegatingHandler
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingHandler"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        public LoggingHandler(ILogger<LoggingHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Request:");
            stringBuilder.AppendLine(request.ToString());
            if (request.Content != null)
            {
                stringBuilder.AppendLine(await request.Content.ReadAsStringAsync());
            }

            stringBuilder.AppendLine();

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            stringBuilder.AppendLine("Response:");
            stringBuilder.AppendLine(response.ToString());
            if (response.Content != null)
            {
                stringBuilder.AppendLine(await response.Content.ReadAsStringAsync());
            }

            stringBuilder.AppendLine();

            this.logger.LogDebug(stringBuilder.ToString());

            return response;
        }
    }
}
