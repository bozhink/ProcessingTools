namespace ProcessingTools.Contracts.Data.Documents.Repositories
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Documents.Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IArticlesRepository : ICrudRepository<IArticle>
    {
        Task<object> AddDocument(object entityId, IDocument document);

        Task<object> RemoveDocument(object entityId, object documentId);

        Task<object> AddAuthor(object entityId, object authorId);

        Task<object> RemoveAuthor(object entityId, object authorId);
    }
}
