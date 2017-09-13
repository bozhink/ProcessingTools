namespace ProcessingTools.Contracts.Data
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface to objects for creation and population with initial data (seed)
    /// of a database context.
    /// </summary>
    public interface IDatabaseSeeder
    {
        /// <summary>
        /// Populates the database context with initial data (seed).
        /// </summary>
        /// <returns>Custom response.</returns>
        Task<object> SeedAsync();
    }
}
