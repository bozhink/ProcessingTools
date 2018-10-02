// <copyright file="FileRepository{TContext,TEntity}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.File.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Code.Data.Expressions;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Data.Expressions;
    using ProcessingTools.Data.Common.File.Contracts;

    /// <summary>
    /// Generic file repository.
    /// </summary>
    /// <typeparam name="TContext">Type of file DB context.</typeparam>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    public abstract class FileRepository<TContext, TEntity> : IFileRepository<TEntity>
        where TContext : IFileDbContext<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileRepository{TContext, TEntity}"/> class.
        /// </summary>
        /// <param name="contextFactory">Context factory.</param>
        protected FileRepository(IFactory<TContext> contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.Context = contextFactory.Create();
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TEntity> Entities => this.Context.DataSet;

        /// <inheritdoc/>
        public virtual IQueryable<TEntity> Query => this.Context.DataSet;

        /// <summary>
        /// Gets the context.
        /// </summary>
        protected virtual TContext Context { get; }

        /// <inheritdoc/>
        public virtual Task<TEntity[]> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return Task.Run(() =>
            {
                var query = this.Context.DataSet.Where(filter);
                var data = query.ToArray();
                return data;
            });
        }

        /// <inheritdoc/>
        public virtual Task<TEntity> FindFirstAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return Task.Run(() => this.Context.DataSet.FirstOrDefault(filter));
        }

        /// <inheritdoc/>
        public virtual Task<TEntity> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.Context.GetAsync(id);
        }

        /// <inheritdoc/>
        public virtual Task<object> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return this.Context.AddAsync(entity);
        }

        /// <inheritdoc/>
        public virtual Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.Context.DeleteAsync(id);
        }

        /// <inheritdoc/>
        public virtual Task<object> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return this.Context.UpdateAsync(entity);
        }

        /// <inheritdoc/>
        public virtual async Task<object> UpdateAsync(object id, IUpdateExpression<TEntity> updateExpression)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (updateExpression == null)
            {
                throw new ArgumentNullException(nameof(updateExpression));
            }

            var entity = await this.GetByIdAsync(id).ConfigureAwait(false);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            var updater = new Updater<TEntity>(updateExpression);
            updater.Invoke(entity);

            return await this.Context.UpdateAsync(entity).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual Task<object> SaveChangesAsync() => Task.FromResult<object>(0);
    }
}
