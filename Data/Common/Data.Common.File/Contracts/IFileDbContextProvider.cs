namespace ProcessingTools.Data.Common.File.Contracts
{
    using ProcessingTools.Contracts.Data;

    public interface IFileDbContextProvider<TContext, T> : IDatabaseProvider<TContext>
        where TContext : IFileDbContext<T>
    {
    }
}
