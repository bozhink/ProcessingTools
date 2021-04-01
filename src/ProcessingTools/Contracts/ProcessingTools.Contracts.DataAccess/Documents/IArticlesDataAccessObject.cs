// <copyright file="IArticlesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.DataAccess.Models.Documents.Articles;
    using ProcessingTools.Contracts.Models.Documents.Articles;

    /// <summary>
    /// Articles data access object.
    /// </summary>
    public interface IArticlesDataAccessObject : IDataAccessObject<IArticleDataTransferObject, IArticleDetailsDataTransferObject, IArticleInsertModel, IArticleUpdateModel>
    {
        /// <summary>
        /// Gets article journals.
        /// </summary>
        /// <returns>Array of journal publishers.</returns>
        Task<IArticleJournalDataTransferObject[]> GetArticleJournalsAsync();

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
        Task<IArticleDataTransferObject> FinalizeAsync(object id);
    }
}
