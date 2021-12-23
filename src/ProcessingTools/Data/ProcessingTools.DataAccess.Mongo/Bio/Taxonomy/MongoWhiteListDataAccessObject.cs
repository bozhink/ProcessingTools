﻿// <copyright file="MongoWhiteListDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DataAccess.Mongo.Bio.Taxonomy
{
    using MongoDB.Driver;
    using ProcessingTools.Contracts.DataAccess.Bio.Taxonomy;
    using ProcessingTools.Data.Models.Mongo.Bio.Taxonomy;

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
