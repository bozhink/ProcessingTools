// <copyright file="IApplicationContext.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    using System;

    /// <summary>
    /// Application contest.
    /// </summary>
    public interface IApplicationContext
    {
        /// <summary>
        /// Gets the user context.
        /// </summary>
        IUserContext UserContext { get; }

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
