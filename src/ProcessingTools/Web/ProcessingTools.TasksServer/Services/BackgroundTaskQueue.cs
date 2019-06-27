// <copyright file="BackgroundTaskQueue.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProcessingTools.TasksServer.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Background task queue.
    /// </summary>
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly ConcurrentQueue<Func<CancellationToken, Task>> workItems = new ConcurrentQueue<Func<CancellationToken, Task>>();
        private readonly SemaphoreSlim signal = new SemaphoreSlim(0);

        /// <inheritdoc/>
        public void EnqueueBackgroundWorkItem(Func<CancellationToken, Task> workItem)
        {
            if (workItem == null)
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
    }
}
