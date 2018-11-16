namespace ProcessingTools.Data.Entity.Abstractions
{
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Data.Contracts;

    /// <summary>
    /// Generic DbContext initializer.
    /// </summary>
    /// <typeparam name="T">Type of the DbContext.</typeparam>
    public interface IDbContextInitializer<T> : IDatabaseInitializer
        where T : DbContext
    {
    }
}
