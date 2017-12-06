// <copyright file="IAuthorsRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Models.Documents;

    /// <summary>
    /// Authors repository.
    /// </summary>
    public interface IAuthorsRepository : ICrudRepository<IAuthor>
    {
        /// <summary>
        /// Adds affiliation to author.
        /// </summary>
        /// <param name="authorId">ID of the author.</param>
        /// <param name="affiliation">Affiliation to be added to the author.</param>
        /// <param name="address">Address of the affiliation.</param>
        /// <returns>Task</returns>
        Task<object> AddAffiliationAsync(object authorId, IAffiliation affiliation, IAddress address);

        /// <summary>
        /// Remove affiliation from author.
        /// </summary>
        /// <param name="authorId">ID of the author.</param>
        /// <param name="affiliationId">ID of the affiliation.</param>
        /// <returns>Task</returns>
        Task<object> RemoveAffiliationAsync(object authorId, object affiliationId);

        /// <summary>
        /// Add authority for an article to an author.
        /// </summary>
        /// <param name="authorId">ID of the author.</param>
        /// <param name="articleId">ID of the article.</param>
        /// <returns>Task</returns>
        Task<object> AddAuthorityForArticleAsync(object authorId, object articleId);

        /// <summary>
        /// Remove authority for an article from an author.
        /// </summary>
        /// <param name="authorId">ID of the author.</param>
        /// <param name="articleId">ID of the article.</param>
        /// <returns>Task</returns>
        Task<object> RemoveAuthorityForArticleAsync(object authorId, object articleId);
    }
}
