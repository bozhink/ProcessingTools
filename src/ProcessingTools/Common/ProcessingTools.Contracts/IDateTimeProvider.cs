// <copyright file="IDateTimeProvider.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    using System;

    /// <summary>
    /// <see cref="DateTime"/> provider.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Gets the value of the now-date-and-time.
        /// </summary>
        DateTime Now { get; }
    }
}
