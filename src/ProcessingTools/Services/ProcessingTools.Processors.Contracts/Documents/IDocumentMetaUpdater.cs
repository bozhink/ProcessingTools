// <copyright file="IDocumentMetaUpdater.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Models.Contracts.Documents.Articles;
    using ProcessingTools.Models.Contracts.Documents.Journals;
    using ProcessingTools.Models.Contracts.Documents.Publishers;

    /// <summary>
    /// Document meta updater.
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
