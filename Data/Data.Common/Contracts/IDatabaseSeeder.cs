namespace ProcessingTools.Data.Common.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface to objects for creation and population with initial data (seed)
    /// of a database context.
    /// </summary>
    public interface IDatabaseSeeder
    {
        /// <summary>
        /// Initializes the database context.
        /// </summary>
        /// <returns>Awaitable Task.</returns>
        Task Init();

        /// <summary>
        /// Populates the database context with initial data (seed).
        /// </summary>
        /// <returns>Awaitable Task.</returns>
        Task Seed();
    }
}