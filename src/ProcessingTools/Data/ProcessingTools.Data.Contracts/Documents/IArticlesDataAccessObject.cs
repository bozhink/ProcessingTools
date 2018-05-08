// <copyright file="IArticlesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Data.Models.Contracts.Documents.Articles;
    using ProcessingTools.Models.Contracts.Documents.Articles;

    /// <summary>
    /// Articles data access object.
    /// </summary>
    public interface IArticlesDataAccessObject : IDataAccessObject<IArticleDataModel, IArticleDetailsDataModel, IArticleInsertModel, IArticleUpdateModel>
    {
        /// <summary>
        /// Gets article journals.
        /// </summary>
        /// <returns>Array of journal publishers.</returns>
        Task<IArticleJournalDataModel[]> GetArticleJournalsAsync();

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
