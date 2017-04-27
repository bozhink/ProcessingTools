namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    public interface IDocumentNormalizer
    {
        Task<object> Normalize(IDocument document);
    }
}
