namespace ProcessingTools.Layout.Processors.Contracts
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public interface IDocumentInitialFormatter
    {
        Task<object> Format(IDocument document);
    }
}
