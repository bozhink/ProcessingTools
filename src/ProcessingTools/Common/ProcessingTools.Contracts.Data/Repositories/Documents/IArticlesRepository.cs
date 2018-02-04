// <copyright file="IArticlesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Models.Contracts.Documents;

    /// <summary>
    /// Articles repository.
    /// </summary>
    public interface IArticlesRepository : ICrudRepository<IArticle>
    {
        /// <summary>
        /// Adds document to article.
        /// </summary>
        /// <param name="articleId">ID of the article to be updated.</param>
        /// <param name="document">Document object to be added.</param>
        /// <returns>Task</returns>
        Task<object> AddDocumentAsync(object articleId, IDocument document);

        /// <summary>
        /// Removes document from article.
        /// </summary>
        /// <param name="articleId">ID of the article to be updated.</param>
        /// <param name="documentId">ID of the document to be removed.</param>
        /// <returns>Task</returns>
        Task<object> RemoveDocumentAsync(object articleId, object documentId);

        /// <summary>
        /// Adds author to article.
        /// </summary>
        /// <param name="articleId">ID of the article to be updated.</param>
        /// <param name="authorId">ID of the author to be assign to article.</param>
        /// <returns>Task</returns>
        Task<object> AddAuthorAsync(object articleId, object authorId);

        /// <summary>
        /// Removes author from article.
        /// </summary>
        /// <param name="articleId">ID of the article to be updated.</param>
        /// <param name="authorId">ID of the author to be assign to article.</param>
        /// <returns>Task</returns>
        Task<object> RemoveAuthorAsync(object articleId, object authorId);
    }
}
