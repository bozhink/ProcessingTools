namespace ProcessingTools.Contracts.Data
{
    public interface IDatabaseProvider<out T>
    {
        T Create();
    }
}
