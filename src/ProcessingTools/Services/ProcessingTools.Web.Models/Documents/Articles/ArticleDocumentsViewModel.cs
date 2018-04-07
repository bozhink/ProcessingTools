// <copyright file="ArticleDocumentsViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Articles
{
    using System.Collections.Generic;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Article documents view model.
    /// </summary>
    public class ArticleDocumentsViewModel : ArticleDetailsViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleDocumentsViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        /// <param name="journal">Selected journal.</param>
        /// <param name="documents">Documents of the article.</param>
        public ArticleDocumentsViewModel(UserContext userContext, ArticleJournalViewModel journal, IEnumerable<ArticleDocumentViewModel> documents)
            : base(userContext, journal)
        {
            this.Documents = documents;
        }

        /// <summary>
        /// Gets the documents of the article.
        /// </summary>
        public IEnumerable<ArticleDocumentViewModel> Documents { get; }
    }
}
