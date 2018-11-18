// <copyright file="InjectionConstants.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Constants
{
    /// <summary>
    /// Constants for IoC and DI configurations.
    /// </summary>
    public static class InjectionConstants
    {
        /// <summary>
        /// Binding name for the MongoDB documents database.
        /// </summary>
        public const string MongoDBDocumentsDatabaseBindingName = nameof(MongoDBDocumentsDatabaseBindingName);

        /// <summary>
        /// Binding name for the MongoDB files database.
        /// </summary>
        public const string MongoDBFilesDatabaseBindingName = nameof(MongoDBFilesDatabaseBindingName);

        /// <summary>
        /// Binding name for the MongoDB documents database.
        /// </summary>
        public const string MongoDBLayoutDatabaseBindingName = nameof(MongoDBLayoutDatabaseBindingName);

        /// <summary>
        /// Binding name for the MongoDB history database.
        /// </summary>
        public const string MongoDBHistoryDatabaseBindingName = nameof(MongoDBHistoryDatabaseBindingName);

        /// <summary>
        /// Connection string parameter name.
        /// </summary>
        public const string ConnectionStringParameterName = "connectionString";

        /// <summary>
        /// Database name parameter name.
        /// </summary>
        public const string DatabaseNameParameterName = "databaseName";
    }
}
