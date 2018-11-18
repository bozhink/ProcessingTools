namespace ProcessingTools.Data.Entity.Abstractions
{
    using Microsoft.EntityFrameworkCore;
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
