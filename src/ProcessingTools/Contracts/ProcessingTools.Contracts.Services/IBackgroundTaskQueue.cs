// <copyright file="IBackgroundTaskQueue.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Background task queue.
    /// </summary>
    public interface IBackgroundTaskQueue
    {
        /// <summary>
        /// Enqueue background work item.
        /// </summary>
        /// <param name="workItem">Work item to be enqueued.</param>
        void EnqueueBackgroundWorkItem(Func<CancellationToken, Task> workItem);

        /// <summary>
        /// Dequeue first background work item.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Work item.</returns>
        Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
    }
}
