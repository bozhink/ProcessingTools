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
    }
}
