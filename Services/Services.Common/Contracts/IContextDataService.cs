namespace ProcessingTools.Services.Common.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using ProcessingTools.Contracts;

    /// <summary>
    /// Represents generic structure of a data service using data connection with context parameter.
    /// </summary>
    /// <typeparam name="TContext">Type of the context object to be accessed.</typeparam>
    /// <typeparam name="TId">Type of the Id property of the service model object.</typeparam>
    /// <typeparam name="TServiceModel">Type of the service model.</typeparam>
    public interface IContextDataService<TContext, TId, TServiceModel>
        where TServiceModel : IGenericIdentifiable<TId>
    {
        /// <summary>
        /// Gets all items from a context.
        /// </summary>
        /// <param name="context">The context object to be accessed.</param>
        /// <returns>Task of IQueryable object.</returns>
        Task<IQueryable<TServiceModel>> All(TContext context);

        /// <summary>
        /// Gets an object by its Id.
        /// </summary>
        /// <param name="context">The context object to be accessed.</param>
        /// <param name="id">Value of the Id of required object.</param>
        /// <returns>Task of an object.</returns>
        Task<TServiceModel> Get(TContext context, TId id);

        /// <summary>
        /// Adds new object to the context.
        /// </summary>
        /// <param name="context">The context object to be accessed.</param>
        /// <param name="model">The object to be added.</param>
        /// <returns>Task to be awaited.</returns>
        Task Add(TContext context, TServiceModel model);

        /// <summary>
        /// Updates existing object in the context.
        /// </summary>
        /// <param name="context">The context object to be accessed.</param>
        /// <param name="model">The new value of the existing object in the context.</param>
        /// <returns>Task to be awaited.</returns>
        Task Update(TContext context, TServiceModel model);

        /// <summary>
        /// Deletes all data in a context and the context.
        /// </summary>
        /// <param name="context">The context object to be deleted.</param>
        /// <returns>Task to be awaited.</returns>
        Task Delete(TContext context);

        /// <summary>
        /// Deletes a specified object from the context by its Id.
        /// </summary>
        /// <param name="context">The context object to be accessed.</param>
        /// <param name="id">Value of the Id of the object to be deleted.</param>
        /// <returns>Task to be awaited.</returns>
        Task Delete(TContext context, TId id);

        /// <summary>
        /// Deletes a specified object from the context.
        /// </summary>
        /// <param name="context">The context object to be accessed.</param>
        /// <param name="model">The object to be deleted.</param>
        /// <returns>Task to be awaited.</returns>
        Task Delete(TContext context, TServiceModel model);
    }
}
