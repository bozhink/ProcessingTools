namespace ProcessingTools.Contracts
{
    public interface IGenericFactory<out T>
    {
        T Create();
    }
}
