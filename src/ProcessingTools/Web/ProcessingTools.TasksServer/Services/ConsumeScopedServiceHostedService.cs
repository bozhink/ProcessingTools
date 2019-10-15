// <copyright file="ConsumeScopedServiceHostedService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.TasksServer.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Common.Resources;
    using ProcessingTools.Contracts.Services;

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
            this.logger.LogInformation(StringResources.ConsumeScopedServiceHostedServiceStarting);

            this.DoWork();

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation(StringResources.ConsumeScopedServiceHostedServiceStopping);

            return Task.CompletedTask;
        }

        private void DoWork()
        {
            this.logger.LogInformation(StringResources.ConsumeScopedServiceHostedServiceWorking);

            using (var scope = this.services.CreateScope())
            {
                var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IScopedProcessingService>();

                scopedProcessingService.DoWork(e => _ = e);
            }
        }
    }
}
