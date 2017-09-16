namespace ProcessingTools.Contracts
{
    public interface IDocumentFactory
    {
        IDocument Create(string content);
    }
}
