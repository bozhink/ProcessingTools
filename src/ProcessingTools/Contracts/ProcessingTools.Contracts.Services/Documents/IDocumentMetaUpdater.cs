// <copyright file="IDocumentMetaUpdater.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Contracts.Models;
using ProcessingTools.Contracts.Models.Documents.Articles;
using ProcessingTools.Contracts.Models.Documents.Journals;
using ProcessingTools.Contracts.Models.Documents.Publishers;

namespace ProcessingTools.Contracts.Services.Documents
{
    /// <summary>
    /// Document meta-data updater.
    /// </summary>
    public interface IDocumentMetaUpdater
    {
        /// <summary>
        /// Updates meta-data of <see cref="IDocument"/> with specified information about the publisher, journal, and article.
        /// </summary>
        /// <param name="document"><see cref="IDocument"/> object to be updated.</param>
        /// <param name="articleMeta">Article meta-data to be applied.</param>
        /// <param name="journalMeta">Journal meta-data to be applied.</param>
        /// <param name="publisherMeta">Publisher meta-data to be applied.</param>
        /// <returns>Task of result.</returns>
        Task<object> UpdateMetaAsync(IDocument document, IArticleMetaModel articleMeta, IJournalMetaModel journalMeta, IPublisherMetaModel publisherMeta);
    }
}
