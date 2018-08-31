// <copyright file="ConnectionStrings.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Constants.Configuration
{
    using System.Configuration;

    /// <summary>
    /// Connection strings
    /// </summary>
    public static class ConnectionStrings
    { /// <summary>
      /// BioDatabaseConnection
      /// </summary>
        public static readonly string BioDatabaseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.BioDatabaseConnection].ConnectionString;

        /// <summary>
        /// BioEnvironmentsDatabaseConnection
        /// </summary>
        public static readonly string BioEnvironmentsDatabaseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.BioEnvironmentsDatabaseConnection].ConnectionString;

        /// <summary>
        /// BioTaxonomyDatabaseConnection
        /// </summary>
        public static readonly string BioTaxonomyDatabaseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.BioTaxonomyDatabaseConnection].ConnectionString;

        /// <summary>
        /// DataDatabseConnection
        /// </summary>
        public static readonly string DataDatabseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.DataDatabseConnection].ConnectionString;

        /// <summary>
        /// DataResourcesDatabaseConnection
        /// </summary>
        public static readonly string DataResourcesDatabaseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.DataResourcesDatabaseConnection].ConnectionString;

        /// <summary>
        /// DefaultConnection
        /// </summary>
        public static readonly string DefaultConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.DefaultConnection].ConnectionString;

        /// <summary>
        /// DocumentsDatabaseConnection
        /// </summary>
        public static readonly string DocumentsDatabaseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.DocumentsDatabaseConnection].ConnectionString;

        /// <summary>
        /// GeoDatabseConnection
        /// </summary>
        public static readonly string GeoDatabseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.GeoDatabseConnection].ConnectionString;

        /// <summary>
        /// HistoryDatabaseConnection
        /// </summary>
        public static readonly string HistoryDatabaseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.HistoryDatabaseConnection].ConnectionString;

        /// <summary>
        /// JournalsDatabaseConnection
        /// </summary>
        public static readonly string JournalsDatabaseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.JournalsDatabaseConnection].ConnectionString;

        /// <summary>
        /// MediatypesDatabaseConnection
        /// </summary>
        public static readonly string MediatypesDatabaseConnection = ConfigurationManager.ConnectionStrings[ConnectionStringsKeys.MediatypesDatabaseConnection].ConnectionString;
    }
}
