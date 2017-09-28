// <copyright file="IAuthorsRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Data.Contracts.Repositories;
    using ProcessingTools.Models.Contracts.Documents;

    /// <summary>
    /// Authors repository.
    /// </summary>
    public interface IAuthorsRepository : ICrudRepository<IAuthor>
    {
        Task<object> AddAffiliation(object entityId, IAffiliation affiliation, IAddress address);

        Task<object> RemoveAffiliation(object entityId, object affiliationId);

        Task<object> AddAuthorityForArticle(object entityId, object articleId);

        Task<object> RemoveAuthorityForArticle(object entityId, object articleId);
    }
}
