namespace ProcessingTools.Documents.Data.Common.Contracts.Repositories
{
    using System.Threading.Tasks;
    using Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IArticlesRepository : ISearchableCountableCrudRepository<IArticleEntity>
    {
        Task<object> AddDocument(object entityId, IDocumentEntity document);

        Task<object> RemoveDocument(object entityId, object documentId);

        Task<object> AddAuthor(object entityId, object authorId);

        Task<object> RemoveAuthor(object entityId, object authorId);
    }
}
