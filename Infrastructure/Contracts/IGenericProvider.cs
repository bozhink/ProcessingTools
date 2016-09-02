namespace ProcessingTools.Contracts
{
    public interface IGenericProvider<T>
    {
        T Create();
    }
}
