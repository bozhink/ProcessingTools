namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Models.Contracts;

    /// <summary>
    /// Generic repository.
    /// </summary>
    /// <typeparam name="TContext">Type of the context object.</typeparam>
    /// <typeparam name="TId">Type of the Id property of the entity model.</typeparam>
    /// <typeparam name="TEntity">Type of the entity model.</typeparam>
    public interface IGenericContextRepository<TContext, TId, TEntity> : IDisposable
        where TEntity : IGenericEntity<TId>
    {
        /// <summary>
        /// Gets all entities in the context.
        /// </summary>
        /// <param name="context">TContext object to be queried.</param>
        /// <returns>All entities in the context.</returns>
        Task<IQueryable<TEntity>> All(TContext context);

        /// <summary>
        /// Gets single entity by its Id.
        /// </summary>
        /// <param name="context">TContext object to be queried.</param>
        /// <param name="id">Value of the Id property of the required entity.</param>
        /// <returns>Single entity with Id property equal to id.</returns>
        Task<TEntity> Get(TContext context, TId id);

        /// <summary>
        /// Adds an entity in the context.
        /// </summary>
        /// <param name="context">TContext object to be queried.</param>
        /// <param name="entity">TEntity object to be added to the context.</param>
        /// <returns>Task to be awaited.</returns>
        Task Add(TContext context, TEntity entity);

        /// <summary>
        /// Updates an entity in the context.
        /// </summary>
        /// <param name="context">TContext object to be queried.</param>
        /// <param name="entity">TEntity object to be updated in the context.</param>
        /// <returns>Task to be awaited.</returns>
        Task Update(TContext context, TEntity entity);

        /// <summary>
        /// Deletes the context or all entities in it.
        /// </summary>
        /// <param name="context">TContext object to be queried.</param>
        /// <returns>Task to be awaited.</returns>
        Task Delete(TContext context);

        /// <summary>
        /// Deletes an entity by its Id in the context.
        /// </summary>
        /// <param name="context">TContext object to be queried.</param>
        /// <param name="id">Value of the Id property of the required entity.</param>
        /// <returns>Task to be awaited.</returns>
        Task Delete(TContext context, TId id);

        /// <summary>
        /// Deletes an entity in the context.
        /// </summary>
        /// <param name="context">TContext object to be queried.</param>
        /// <param name="entity">TEntity object to be deleted in the context.</param>
        /// <returns>Task to be awaited.</returns>
        Task Delete(TContext context, TEntity entity);

        /// <summary>
        /// Save changed made to context’s items.
        /// </summary>
        /// <param name="context">TContext object to be queried.</param>
        /// <returns>Status code.</returns>
        Task<int> SaveChanges(TContext context);
    }
}
