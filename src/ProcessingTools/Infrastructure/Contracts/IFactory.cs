namespace ProcessingTools.Contracts
{
    public interface IFactory<out T>
    {
        T Create();
    }
}
