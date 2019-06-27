// <copyright file="EnvironmentService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services
{
    using System;
    using ProcessingTools.Services.Contracts;

    /// <summary>
    /// Environment service.
    /// </summary>
    public class EnvironmentService : IEnvironmentService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentService"/> class.
        /// </summary>
        public EnvironmentService()
        {
            this.SystemTransactionId = Guid.NewGuid().ToString();
        }

        /// <inheritdoc/>
        public string SystemTransactionId { get; }
    }
}
