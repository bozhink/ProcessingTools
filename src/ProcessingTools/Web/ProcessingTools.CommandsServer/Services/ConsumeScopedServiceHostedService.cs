// <copyright file="ConsumeScopedServiceHostedService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.CommandsServer.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Common.Resources;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Cache;

    /// <summary>
    /// Consume scoped service hosted service.
    /// </summary>
    /// <typeparam name="T">Type of the invoked scoped processing service.</typeparam>
    public class ConsumeScopedServiceHostedService<T> : IHostedService, IDisposable
        where T : IScopedProcessingService
    {
        private readonly IServiceProvider services;
        private readonly IMessageCacheService messageCacheService;
        private readonly ILogger logger;
        private T scopedProcessingService;
        private Timer timer;
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumeScopedServiceHostedService{T}"/> class.
        /// </summary>
        /// <param name="services">Instance of <see cref="IServiceProvider"/>.</param>
        /// <param name="messageCacheService">Instance of <see cref="IMessageCacheService"/>.</param>
        /// <param name="logger">Logger.</param>
        public ConsumeScopedServiceHostedService(IServiceProvider services, IMessageCacheService messageCacheService, ILogger<ConsumeScopedServiceHostedService<T>> logger)
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
            this.messageCacheService = messageCacheService ?? throw new ArgumentNullException(nameof(messageCacheService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ConsumeScopedServiceHostedService{T}"/> class.
        /// </summary>
        ~ConsumeScopedServiceHostedService()
        {
            this.Dispose(false);
        }

        /// <inheritdoc/>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation(StringResources.ConsumeScopedServiceHostedServiceStarting);

            this.timer = new Timer(this.DoWork, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation(StringResources.ConsumeScopedServiceHostedServiceStopping);

            this.StopScopedService();
            this.timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Public implementation of Dispose pattern callable by consumers.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
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
                this.timer?.Dispose();
            }

            /*
             * Free any unmanaged objects here.
             */
            this.disposed = true;
        }

        private void DoWork(object state)
        {
            this.timer?.Change(Timeout.Infinite, 0);

            try
            {
                this.RunScopedService();
            }
            catch (Exception ex)
            {
                this.messageCacheService.Message = ex.Message;
                this.messageCacheService.Exception = ex;

                this.logger.LogError(ex, StringResources.ConsumeScopedServiceHostedServiceStartError);

                this.StopScopedService();

                this.timer?.Change(TimeSpan.Zero, TimeSpan.FromSeconds(5));
            }
        }

        private void RunScopedService()
        {
            this.logger.LogInformation(StringResources.ConsumeScopedServiceHostedServiceWorking);

            using (var scope = this.services.CreateScope())
            {
                this.scopedProcessingService = scope.ServiceProvider.GetRequiredService<T>();
                this.scopedProcessingService.Start();
                this.scopedProcessingService.DoWork(this.ExceptionHandler);
            }
        }

        private void ExceptionHandler(Exception ex)
        {
            this.messageCacheService.Message = ex.Message;
            this.messageCacheService.Exception = ex;

            this.logger.LogError(ex, string.Empty);

            this.StopScopedService();

            this.timer?.Change(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
        }

        private void StopScopedService()
        {
            if (this.scopedProcessingService != null)
            {
                try
                {
                    this.scopedProcessingService.Stop();
                }
                catch (Exception ex)
                {
                    this.messageCacheService.Message = ex.Message;
                    this.messageCacheService.Exception = ex;

                    this.logger.LogError(ex, StringResources.ConsumeScopedServiceHostedServiceStopError);
                }
            }
        }
    }
}
