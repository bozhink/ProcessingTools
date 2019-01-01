// <copyright file="ConsumeScopedServiceHostedService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProcessingTools.TaskServer.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Consume scoped service hosted service.
    /// </summary>
    internal class ConsumeScopedServiceHostedService : IHostedService
    {
        private readonly IServiceProvider services;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumeScopedServiceHostedService"/> class.
        /// </summary>
        /// <param name="services">Service provider.</param>
        /// <param name="logger">Logger.</param>
        public ConsumeScopedServiceHostedService(IServiceProvider services, ILogger<ConsumeScopedServiceHostedService> logger)
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("Consume Scoped Service Hosted Service is starting.");

            this.DoWork();

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation(
                "Consume Scoped Service Hosted Service is stopping.");

            return Task.CompletedTask;
        }

        private void DoWork()
        {
            this.logger.LogInformation("Consume Scoped Service Hosted Service is working.");

            using (var scope = this.services.CreateScope())
            {
                var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IScopedProcessingService>();

                scopedProcessingService.DoWork();
            }
        }
    }
}
