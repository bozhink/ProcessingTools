// <copyright file="MongoCrudRepository{TM,TE}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Mongo.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Contracts.Data.Expressions;
    using ProcessingTools.Data.Common.Mongo.Contracts;

    /// <summary>
    /// MongoDB CRUD repository.
    /// </summary>
    /// <typeparam name="TM">Type of the database model.</typeparam>
    /// <typeparam name="TE">Type of the entity.</typeparam>
    public abstract class MongoCrudRepository<TM, TE> : MongoRepository<TM>, IMongoCrudRepository<TE>, IMongoSearchableRepository<TE>
        where TE : class
        where TM : class, TE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoCrudRepository{TM, TE}"/> class.
        /// </summary>
        /// <param name="databaseProvider">Database provider.</param>
        protected MongoCrudRepository(IMongoDatabaseProvider databaseProvider)
            : base(databaseProvider)
        {
        }

        /// <inheritdoc/>
        public virtual IQueryable<TE> Query => this.Collection.AsQueryable().AsQueryable<TE>();

        /// <inheritdoc/>
        public abstract Task<object> AddAsync(TE entity);

        /// <inheritdoc/>
        public virtual async Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = MongoUtilities.GetFilterById<TM>(id);
            var result = await this.Collection.DeleteOneAsync(filter).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        // TODO
        public virtual Task<TE[]> FindAsync(Expression<Func<TE, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var query = this.Collection.AsQueryable().Where(filter);
            var data = query.ToArray();
            return Task.FromResult(data);
        }

        /// <inheritdoc/>
        public virtual Task<TE> FindFirstAsync(Expression<Func<TE, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            // TODO
            var entity = this.Collection
                 .AsQueryable()
                 .FirstOrDefault(filter);
            return Task.FromResult(entity);
        }

        /// <inheritdoc/>
        public async Task<TE> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = MongoUtilities.GetFilterById<TM>(id);
            var entity = await this.Collection.Find(filter).FirstOrDefaultAsync().ConfigureAwait(false);
            return entity;
        }

        /// <inheritdoc/>
        public abstract Task<object> UpdateAsync(TE entity);

        /// <inheritdoc/>
        public virtual async Task<object> UpdateAsync(object id, IUpdateExpression<TE> updateExpression)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (updateExpression == null)
            {
                throw new ArgumentNullException(nameof(updateExpression));
            }

            var updateQuery = MongoUtilities.ConvertUpdateExpressionToMongoUpdateQuery<TM, TE>(updateExpression);
            var filter = MongoUtilities.GetFilterById<TM>(id);
            var result = await this.Collection.UpdateOneAsync(filter, updateQuery).ConfigureAwait(false);
            return result;
        }
    }
}
