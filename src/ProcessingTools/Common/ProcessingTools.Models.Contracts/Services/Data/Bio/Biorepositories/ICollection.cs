// <copyright file="ICollection.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Services.Data.Bio.Biorepositories
{
    /// <summary>
    /// Biorepositories collection service model.
    /// </summary>
    public interface ICollection
    {
        /// <summary>
        /// Gets collection code.
        /// </summary>
        string Code { get; }

        /// <summary>
        /// Gets collection name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets collection URL.
        /// </summary>
        string Url { get; }
    }
}
