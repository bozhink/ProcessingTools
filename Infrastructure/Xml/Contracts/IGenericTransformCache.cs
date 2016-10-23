namespace ProcessingTools.Xml.Contracts
{
    public interface IGenericTransformCache<T>
    {
        T this[string fileName] { get; }

        bool Remove(string fileName);

        bool RemoveAll();
    }
}
