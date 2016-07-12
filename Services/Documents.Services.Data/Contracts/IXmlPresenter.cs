namespace ProcessingTools.Documents.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using Models;

    public interface IXmlPresenter
    {
        Task<string> GetHtml(IDocumentsDataService service, object userId, object articleId, object documentId);

        Task<string> GetXml(IDocumentsDataService service, object userId, object articleId, object documentId);

        Task<object> SaveHtml(IDocumentsDataService service, object userId, object articleId, DocumentServiceModel document, string content);

        Task<object> SaveXml(IDocumentsDataService service, object userId, object articleId, DocumentServiceModel document, string content);
    }
}
