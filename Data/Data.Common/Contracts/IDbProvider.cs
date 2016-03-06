namespace ProcessingTools.Data.Common.Contracts
{
    public interface IDbProvider<T>
    {
        T Create();
    }
}