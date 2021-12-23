// <copyright file="BackgroundTaskQueue.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services;

    /// <summary>
    /// Background task queue.
    /// </summary>
    public class BackgroundTaskQueue : IBackgroundTaskQueue, IDisposable
    {
        private readonly ConcurrentQueue<Func<CancellationToken, Task>> workItems = new ConcurrentQueue<Func<CancellationToken, Task>>();
        private readonly SemaphoreSlim signal;

        // Flag: Has Dispose already been called?
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundTaskQueue"/> class.
        /// </summary>
        public BackgroundTaskQueue()
        {
            this.signal = new SemaphoreSlim(0);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="BackgroundTaskQueue"/> class.
        /// </summary>
        ~BackgroundTaskQueue()
        {
            this.Dispose(false);
        }

        /// <inheritdoc/>
        public void EnqueueBackgroundWorkItem(Func<CancellationToken, Task> workItem)
        {
            if (workItem is null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            this.workItems.Enqueue(workItem);
            this.signal.Release();
        }

        /// <inheritdoc/>
        public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            await this.signal.WaitAsync(cancellationToken).ConfigureAwait(false);
            this.workItems.TryDequeue(out var workItem);

            return workItem;
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
        /// Dispose unmanaged resources.
        /// </summary>
        /// <param name="disposing">Value that indicates whether the method call comes from a Dispose method (its value is true) or from a finalizer (its value is false).</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                // Free any managed objects here.
                this.signal.Dispose();
            }

            this.disposed = true;
        }
    }
}
