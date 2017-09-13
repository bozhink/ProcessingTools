namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    public interface IDocumentNormalizer
    {
        Task<object> NormalizeAsync(IDocument document);
    }
}
