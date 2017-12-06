namespace ProcessingTools.Contracts.Services.Data.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services.Models.Data.Documents;

    public interface IXmlPresenter
    {
        Task<string> GetHtml(object userId, object articleId, object documentId);

        Task<string> GetXml(object userId, object articleId, object documentId);

        Task<object> SaveHtml(object userId, object articleId, IDocument document, string content);

        Task<object> SaveXml(object userId, object articleId, IDocument document, string content);
    }
}
