// <copyright file="IArticleDocumentsFullModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Meta
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts.Documents.Documents;

    /// <summary>
    /// Article documents full model.
    /// </summary>
    public interface IArticleDocumentsFullModel : IArticleFullModel
    {
        /// <summary>
        /// Gets documents of the article.
        /// </summary>
        IEnumerable<IDocumentModel> Documents { get; }
    }
}
