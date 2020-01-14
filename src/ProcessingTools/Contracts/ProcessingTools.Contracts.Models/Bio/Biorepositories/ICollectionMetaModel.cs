// <copyright file="ICollectionMetaModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Bio.Biorepositories
{
    /// <summary>
    /// Biorepositories collection service model.
    /// </summary>
    public interface ICollectionMetaModel
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
