// <copyright file="MongoUtilities.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo
{
    using System;
    using System.Linq;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using ProcessingTools.Contracts.Data.Expressions;

    /// <summary>
    /// MongoDB utilities.
    /// </summary>
    internal static class MongoUtilities
    {
        /// <summary>
        /// Gets filter definition for the ID property.
        /// </summary>
        /// <typeparam name="T">Type of model.</typeparam>
        /// <param name="id">The ID (_id) of the model.</param>
        /// <returns>Filter definition.</returns>
        public static FilterDefinition<T> GetFilterById<T>(object id)
        {
            var objectId = new ObjectId(id.ToString());
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            return filter;
        }

        /// <summary>
        /// Converts update expression to MongoDB update definition.
        /// </summary>
        /// <typeparam name="TM">Type of database model.</typeparam>
        /// <typeparam name="TE">Type of entity.</typeparam>
        /// <param name="updateExpression">The update expression.</param>
        /// <returns>The resultant update definition.</returns>
        public static UpdateDefinition<TM> ConvertUpdateExpressionToMongoUpdateQuery<TM, TE>(IUpdateExpression<TE> updateExpression)
            where TE : class
            where TM : class, TE
        {
            var updateCommands = updateExpression.UpdateCommands.ToArray();
            if (updateCommands.Length < 1)
            {
                throw new ArgumentNullException(nameof(updateExpression));
            }

            var updateCommand = updateCommands[0];
            var updateQuery = Builders<TM>.Update
                .Set(updateCommand.FieldName, updateCommand.Value);
            for (int i = 1; i < updateCommands.Length; ++i)
            {
                updateCommand = updateCommands[i];
                updateQuery = updateQuery.Set(updateCommand.FieldName, updateCommand.Value);
            }

            return updateQuery;
        }
    }
}
