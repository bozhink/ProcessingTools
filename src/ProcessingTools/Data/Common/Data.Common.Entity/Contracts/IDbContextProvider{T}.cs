namespace ProcessingTools.Data.Common.Entity.Contracts
{
    using System.Data.Entity;
    using ProcessingTools.Data.Contracts;

    /// <summary>
    /// Generic DbContext provider.
    /// </summary>
    /// <typeparam name="T">Type of the DbContext.</typeparam>
    public interface IDbContextProvider<out T> : IDatabaseProvider<T>
        where T : DbContext
    {
    }
}
