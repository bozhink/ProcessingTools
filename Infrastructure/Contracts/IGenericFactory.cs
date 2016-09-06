namespace ProcessingTools.Contracts
{
    public interface IGenericFactory<T>
    {
        T Create();
    }
}
