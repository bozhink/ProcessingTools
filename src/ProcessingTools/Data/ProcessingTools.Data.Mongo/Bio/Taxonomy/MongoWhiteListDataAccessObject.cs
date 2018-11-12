// <copyright file="MongoWhiteListDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Bio.Taxonomy.Mongo
{
    using MongoDB.Driver;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Models;
    using ProcessingTools.Data.Contracts.Bio.Taxonomy;

    /// <summary>
    /// MongoDB implementation of <see cref="IWhiteListDataAccessObject"/>.
    /// </summary>
    public class MongoWhiteListDataAccessObject : MongoStringListDataAccessObject<WhiteListItem>, IWhiteListDataAccessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoWhiteListDataAccessObject"/> class.
        /// </summary>
        /// <param name="collection">MongoDB collection of the white list items.</param>
        public MongoWhiteListDataAccessObject(IMongoCollection<WhiteListItem> collection)
            : base(collection)
        {
        }
    }
}
