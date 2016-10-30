namespace ProcessingTools.Documents.Data.Common.Repositories.Contracts
{
    using System.Threading.Tasks;
    using Models.Contracts;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IArticlesRepository : IGenericRepository<IArticleEntity>
    {
        Task<object> AddDocument(object entityId, IDocumentEntity document);

        Task<object> RemoveDocument(object entityId, object documentId);

        Task<object> AddAuthor(object entityId, object authorId);

        Task<object> RemoveAuthor(object entityId, object authorId);
    }
}
