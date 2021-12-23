// <copyright file="IEnvironmentService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Environment service.
    /// </summary>
    public interface IEnvironmentService
    {
        /// <summary>
        /// Gets the system transaction ID.
        /// </summary>
        string SystemTransactionId { get; }
    }
}
