// <copyright file="DateTimeProvider.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Providers
{
    using System;
    using ProcessingTools.Contracts;

    /// <summary>
    /// DateTimeProvider. TODO: remove
    /// </summary>
    public class DateTimeProvider : IDateTimeProvider
    {

        /// <inheritdoc/>
        public DateTime Now => DateTime.UtcNow;
    }
}
