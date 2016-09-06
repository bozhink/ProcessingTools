namespace ProcessingTools.Contracts
{
    public interface IGenericDocumentFactory<T> : IGenericFactory<T>
        where T : IDocument
    {
        T Create(string content);
    }
}
