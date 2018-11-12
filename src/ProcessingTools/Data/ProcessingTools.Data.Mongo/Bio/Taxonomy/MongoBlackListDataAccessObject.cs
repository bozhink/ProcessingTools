// <copyright file="MongoBlackListDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Bio.Taxonomy
{
    using MongoDB.Driver;
    using ProcessingTools.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Data.Models.Mongo.Bio.Taxonomy;

    /// <summary>
    /// MongoDB implementation of <see cref="IBlackListDataAccessObject"/>.
    /// </summary>
    public class MongoBlackListDataAccessObject : MongoStringListDataAccessObject<BlackListItem>, IBlackListDataAccessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoBlackListDataAccessObject"/> class.
        /// </summary>
        /// <param name="collection">MongoDB collection of the black list items.</param>
        public MongoBlackListDataAccessObject(IMongoCollection<BlackListItem> collection)
            : base(collection)
        {
        }
    }
}
