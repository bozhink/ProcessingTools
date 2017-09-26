// <copyright file="IAuthor.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Documents.Models
{
    using ProcessingTools.Models.Contracts;
    using System.Collections.Generic;

    /// <summary>
    /// Author.
    /// </summary>
    public interface IAuthor : IPerson, IGuidIdentifiable, IModelWithUserInformation
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
