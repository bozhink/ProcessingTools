namespace ProcessingTools.Contracts
{
    public interface IGenericDocumentFactory<out T> : IGenericFactory<T>
        where T : IDocument
    {
        T Create(string content);
    }
}
