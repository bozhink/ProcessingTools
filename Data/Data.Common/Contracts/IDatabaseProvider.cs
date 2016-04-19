namespace ProcessingTools.Data.Common.Contracts
{
    public interface IDatabaseProvider<T>
    {
        T Create();
    }
}