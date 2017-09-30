// <copyright file="GuidProvider.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Providers
{
    using System;
    using ProcessingTools.Contracts;

    /// <summary>
    /// GUID provider. TODO: remove
    /// </summary>
    public class GuidProvider : IGuidProvider
    {
        /// <inheritdoc/>
        public Guid NewGuid() => Guid.NewGuid();
    }
}
