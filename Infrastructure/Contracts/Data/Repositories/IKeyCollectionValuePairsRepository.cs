namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a [key]-[collection of values] pairs data repository.
    /// </summary>
    /// <typeparam name="TKey">Type of the key-objects.</typeparam>
    /// <typeparam name="TValue">Type of value-objects in the collection of values.</typeparam>
    public interface IKeyCollectionValuePairsRepository<TKey, TValue> : IRepository<TValue>, ISavabaleRepository
    {
        /// <summary>
        /// Adds new value to a given key.
        /// </summary>
        /// <param name="key">The key-object to be created.</param>
        /// <param name="value">The value-object to be set as value to the key.</param>
        /// <returns></returns>
        Task<object> Add(TKey key, TValue value);

        /// <summary>
        /// Gets all values under the key-object.
        /// </summary>
        /// <param name="key">The key-object to be requested.</param>
        /// <returns>The collection of values under the requested key-object.</returns>
        IEnumerable<TValue> GetAll(TKey key);

        /// <summary>
        /// Removes the key-object and its underlying collection of values.
        /// </summary>
        /// <param name="key">The key object to be removed.</param>
        /// <returns></returns>
        Task<object> Remove(TKey key);

        /// <summary>
        /// Removes a value from the collection of values under a key.
        /// </summary>
        /// <param name="key">The key-object to be requested.</param>
        /// <param name="value">The value-object to be removed from the collection of values.</param>
        /// <returns></returns>
        Task<object> Remove(TKey key, TValue value);
    }
}
