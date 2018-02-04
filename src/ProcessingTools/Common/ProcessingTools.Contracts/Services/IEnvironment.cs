// <copyright file="IEnvironment.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using System;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Provider executing environment settings.
    /// </summary>
    public interface IEnvironment
    {
        /// <summary>
        /// Gets the environment user.
        /// </summary>
        IEnvironmentUser User { get; }

        /// <summary>
        /// Gets <see cref="DateTime"/> provider.
        /// </summary>
        Func<DateTime> DateTimeProvider { get; }

        /// <summary>
        /// Gets <see cref="Guid"/> provider.
        /// </summary>
        Func<Guid> GuidProvider { get; }
    }
}
