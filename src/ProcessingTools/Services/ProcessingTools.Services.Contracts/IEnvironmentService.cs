// <copyright file="IEnvironmentService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts
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
