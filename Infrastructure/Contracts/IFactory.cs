namespace ProcessingTools.Contracts
{
    public interface IFactory<T>
    {
        T Create();
    }
}
