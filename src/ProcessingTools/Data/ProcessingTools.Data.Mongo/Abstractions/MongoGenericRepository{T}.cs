// <copyright file="MongoGenericRepository{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Abstractions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Driver;
    using ProcessingTools.Contracts.Data.Expressions;
    using ProcessingTools.Extensions.Data;

    /// <summary>
    /// Generic MongoDB repository.
    /// </summary>
    /// <typeparam name="T">Type of database model.</typeparam>
    public class MongoGenericRepository<T> : MongoRepository<T>, IMongoGenericRepository<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoGenericRepository{T}"/> class.
        /// </summary>
        /// <param name="databaseProvider">Database provider.</param>
        public MongoGenericRepository(IMongoDatabaseProvider databaseProvider)
            : base(databaseProvider)
        {
        }

        /// <inheritdoc/>
        public virtual IQueryable<T> Query => this.Collection.AsQueryable();

        /// <inheritdoc/>
        public virtual async Task<object> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await this.Collection.InsertOneAsync(entity).ConfigureAwait(false);
            return entity;
        }

        /// <inheritdoc/>
        public virtual async Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = MongoUtilities.GetFilterById<T>(id);
            var result = await this.Collection.DeleteOneAsync(filter).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<object> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var id = entity.GetIdValue<BsonIdAttribute>();
            var filter = MongoUtilities.GetFilterById<T>(id);
            var result = await this.Collection.ReplaceOneAsync(filter, entity).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<object> UpdateAsync(object id, IUpdateExpression<T> updateExpression)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (updateExpression == null)
            {
                throw new ArgumentNullException(nameof(updateExpression));
            }

            var updateQuery = MongoUtilities.ConvertUpdateExpressionToMongoUpdateQuery<T, T>(updateExpression);
            var filter = MongoUtilities.GetFilterById<T>(id);
            var result = await this.Collection.UpdateOneAsync(filter, updateQuery).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<T[]> FindAsync(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var cursor = await this.Collection.FindAsync(filter).ConfigureAwait(false);
            var data = cursor.ToList().ToArray();
            return data;
        }

        /// <inheritdoc/>
        public virtual async Task<T> FindFirstAsync(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var entity = await this.Collection.Find(filter).FirstOrDefaultAsync().ConfigureAwait(false);
            return entity;
        }

        /// <inheritdoc/>
        public async Task<T> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = MongoUtilities.GetFilterById<T>(id);
            var entity = await this.Collection.Find(filter).FirstOrDefaultAsync().ConfigureAwait(false);
            return entity;
        }
    }
}
