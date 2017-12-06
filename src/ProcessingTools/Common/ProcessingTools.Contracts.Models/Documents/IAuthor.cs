// <copyright file="IAuthor.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Documents
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Author.
    /// </summary>
    public interface IAuthor : IPerson, IGuidIdentifiable, ICreated, IModified
    {
        /// <summary>
        /// Gets affiliations.
        /// </summary>
        IEnumerable<IAffiliation> Affiliations { get; }

        /// <summary>
        /// Gets articles.
        /// </summary>
        IEnumerable<IArticle> Articles { get; }
    }
}
