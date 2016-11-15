namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Services
{
    using Contracts.Models;
    using ProcessingTools.Contracts.Harvesters;

    public interface IArticleMetaHarvester : IGenericDocumentHarvester<IArticle>
    {
    }
}
