namespace ProcessingTools.Xml.Contracts.Cache
{
    public interface IGenericTransformCache<out T>
    {
        T this[string fileName] { get; }

        bool Remove(string fileName);

        bool RemoveAll();
    }
}
