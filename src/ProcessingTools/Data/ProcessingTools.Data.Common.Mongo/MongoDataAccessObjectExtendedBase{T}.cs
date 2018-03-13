// <copyright file="MongoDataAccessObjectExtendedBase{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Mongo
{
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// MongoDB extended base for data access object (DAO).
    /// </summary>
    /// <typeparam name="T">Type of database entity.</typeparam>
    public class MongoDataAccessObjectExtendedBase<T> : MongoDataAccessObjectBase<T>
        where T : class, IStringIdentifiable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDataAccessObjectExtendedBase{T}"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        protected MongoDataAccessObjectExtendedBase(IMongoDatabaseProvider databaseProvider)
            : base(databaseProvider)
        {
        }

        /// <summary>
        /// Delete item from the data store.
        /// </summary>
        /// <param name="id">ID of the item to be deleted.</param>
        /// <returns>Task</returns>
        public virtual async Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                return null;
            }

            var result = await this.Collection.DeleteOneAsync(p => p.Id == id.ToString()).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Selects the count of all items in the data store.
        /// </summary>
        /// <returns>The long count of all items in the data store.</returns>
        public virtual Task<long> SelectCountAsync()
        {
            return this.Collection.CountAsync(p => true);
        }
    }
}
