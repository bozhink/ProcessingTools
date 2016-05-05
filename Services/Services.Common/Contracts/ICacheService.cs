namespace ProcessingTools.Services.Common.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using Models.Contracts;

    /// <summary>
    /// Represents generic structure of an caching service.
    /// </summary>
    /// <typeparam name="TContext">Type of the context object to be accessed.</typeparam>
    /// <typeparam name="TId">Type of the Id property of the cached-model object.</typeparam>
    /// <typeparam name="TServiceModel">Type of the object to be cached.</typeparam>
    public interface ICacheService<TContext, TId, TServiceModel>
        where TServiceModel : IGenericServiceModel<TId>
    {
        /// <summary>
        /// Gets all items from a context.
        /// </summary>
        /// <param name="context">The context object to be accessed.</param>
        /// <returns>Task of IQueryable cached objects.</returns>
        Task<IQueryable<TServiceModel>> All(TContext context);

        /// <summary>
        /// Gets a cached object by its Id.
        /// </summary>
        /// <param name="context">The context object to be accessed.</param>
        /// <param name="id">Value of the Id of required object.</param>
        /// <returns>Task of a cached object.</returns>
        Task<TServiceModel> Get(TContext context, TId id);

        /// <summary>
        /// Adds new object to the cache.
        /// </summary>
        /// <param name="context">The context object to be accessed.</param>
        /// <param name="model">The object to be cached.</param>
        /// <returns>Task to be awaited.</returns>
        Task Add(TContext context, TServiceModel model);

        /// <summary>
        /// Updates known object form the cache.
        /// </summary>
        /// <param name="context">The context object to be accessed.</param>
        /// <param name="model">The new value of the known object from the cache.</param>
        /// <returns>Task to be awaited.</returns>
        Task Update(TContext context, TServiceModel model);

        /// <summary>
        /// Deletes all cached data in a context and the context.
        /// </summary>
        /// <param name="context">The context object to be accessed.</param>
        /// <returns>Task to be awaited.</returns>
        Task Delete(TContext context);

        /// <summary>
        /// Delete a specified object from the cache by its Id.
        /// </summary>
        /// <param name="context">The context object to be accessed.</param>
        /// <param name="id">Value of the Id of the cached object to be deleted.</param>
        /// <returns>Task to be awaited.</returns>
        Task Delete(TContext context, TId id);

        /// <summary>
        /// Delete a specified object from the cache.
        /// </summary>
        /// <param name="context">The context object to be accessed.</param>
        /// <param name="model">The cached object to be deleted.</param>
        /// <returns>Task to be awaited.</returns>
        Task Delete(TContext context, TServiceModel model);
    }
}
