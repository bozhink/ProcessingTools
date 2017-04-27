namespace ProcessingTools.Contracts.Data.Documents.Repositories
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Documents.Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IAuthorsRepository : ICrudRepository<IAuthorEntity>
    {
        Task<object> AddAffiliation(object entityId, IAffiliationEntity affiliation, IAddressEntity address);

        Task<object> RemoveAffiliation(object entityId, object affiliationId);

        Task<object> AddAuthorityForArticle(object entityId, object articleId);

        Task<object> RemoveAuthorityForArticle(object entityId, object articleId);
    }
}
