// <copyright file="IGuidProvider.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    using System;

    /// <summary>
    /// <see cref="Guid"/> provider.
    /// </summary>
    public interface IGuidProvider
    {
        /// <summary>
        /// Generates new <see cref="Guid"/> object.
        /// </summary>
        /// <returns>New <see cref="Guid"/> object.</returns>
        Guid NewGuid();
    }
}
