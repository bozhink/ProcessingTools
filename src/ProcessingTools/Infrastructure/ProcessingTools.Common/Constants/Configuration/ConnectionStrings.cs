// <copyright file="ConnectionStrings.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Constants.Configuration
{
    using System.Configuration;

    /// <summary>
    /// Connection strings.
    /// </summary>
    public static class ConnectionStrings
    {
        /// <summary>
        /// Bio-database connection.
        /// </summary>
        public static readonly string BioDatabaseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.BioDatabaseConnection].ConnectionString;

        /// <summary>
        /// Bio-environments database connection.
        /// </summary>
        public static readonly string BioEnvironmentsDatabaseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.BioEnvironmentsDatabaseConnection].ConnectionString;

        /// <summary>
        /// Bio-taxonomy database connection.
        /// </summary>
        public static readonly string BioTaxonomyDatabaseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.BioTaxonomyDatabaseConnection].ConnectionString;

        /// <summary>
        /// Data database connection.
        /// </summary>
        public static readonly string DataDatabaseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.DataDatabaseConnection].ConnectionString;

        /// <summary>
        /// Data resources database connection.
        /// </summary>
        public static readonly string DataResourcesDatabaseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.DataResourcesDatabaseConnection].ConnectionString;

        /// <summary>
        /// Default connection.
        /// </summary>
        public static readonly string DefaultConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.DefaultConnection].ConnectionString;

        /// <summary>
        /// Documents database connection.
        /// </summary>
        public static readonly string DocumentsDatabaseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.DocumentsDatabaseConnection].ConnectionString;

        /// <summary>
        /// Geo database connection.
        /// </summary>
        public static readonly string GeoDatabaseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.GeoDatabaseConnection].ConnectionString;

        /// <summary>
        /// History database connection.
        /// </summary>
        public static readonly string HistoryDatabaseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.HistoryDatabaseConnection].ConnectionString;

        /// <summary>
        /// Journals database connection.
        /// </summary>
        public static readonly string JournalsDatabaseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.JournalsDatabaseConnection].ConnectionString;

        /// <summary>
        /// Mediatypes database connection.
        /// </summary>
        public static readonly string MediatypesDatabaseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.MediatypesDatabaseConnection].ConnectionString;
    }
}
