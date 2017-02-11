namespace ProcessingTools.Documents.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using Models;

    public interface IXmlPresenter
    {
        Task<string> GetHtml(object userId, object articleId, object documentId);

        Task<string> GetXml(object userId, object articleId, object documentId);

        Task<object> SaveHtml(object userId, object articleId, IDocumentServiceModel document, string content);

        Task<object> SaveXml(object userId, object articleId, IDocumentServiceModel document, string content);
    }
}
