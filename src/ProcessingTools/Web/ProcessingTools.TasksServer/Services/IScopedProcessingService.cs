// <copyright file="IScopedProcessingService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.TasksServer.Services
{
    /// <summary>
    /// Scoped processing service.
    /// </summary>
    internal interface IScopedProcessingService
    {
        /// <summary>
        /// Do work.
        /// </summary>
        void DoWork();
    }
}
