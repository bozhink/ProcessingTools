// <copyright file="IArticlesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Documents
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services.Models.Documents.Articles;

    /// <summary>
    /// Articles data service.
    /// </summary>
    public interface IArticlesDataService : IDataService<IArticleModel, IArticleDetailsModel, IArticleInsertModel, IArticleUpdateModel>
    {
        /// <summary>
        /// Gets article journals for select.
        /// </summary>
        /// <returns>Array of article journals.</returns>
        Task<IList<IArticleJournalModel>> GetArticleJournalsAsync();

        /// <summary>
        /// Gets the object ID of the journal style for a specified article.
        /// </summary>
        /// <param name="id">Object ID of the article.</param>
        /// <returns>Object ID of the journal style for the article.</returns>
        Task<object> GetJournalStyleIdAsync(object id);

        /// <summary>
        /// Finalizes article specified by object ID.
        /// </summary>
        /// <param name="id">Object ID of the article.</param>
        /// <returns>Resultant object.</returns>
        Task<object> FinalizeAsync(object id);
    }
}
