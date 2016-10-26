namespace ProcessingTools.Contracts.Data
{
    public interface IDatabaseProvider<T>
    {
        T Database { get; }
    }
}
