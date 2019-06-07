// <copyright file="IArticleDocumentsFullModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Documents.Meta
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.Documents.Documents;

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
