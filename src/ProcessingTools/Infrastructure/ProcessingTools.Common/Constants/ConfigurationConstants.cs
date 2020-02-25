// <copyright file="ConfigurationConstants.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Constants
{
    /// <summary>
    /// Configuration constants.
    /// </summary>
    public static class ConfigurationConstants
    {
        /// <summary>
        /// Abbreviations XQuery file path.
        /// </summary>
        public const string AbbreviationsXQueryFilePath = "StaticFiles:AbbreviationsXQueryFilePath";

        /// <summary>
        /// Facebook application ID.
        /// </summary>
        public const string AuthenticationFacebookAppId = "Authentication:Facebook:AppId";

        /// <summary>
        /// Facebook application secret.
        /// </summary>
        public const string AuthenticationFacebookAppSecret = "Authentication:Facebook:AppSecret";

        /// <summary>
        /// Google client ID.
        /// </summary>
        public const string AuthenticationGoogleClientId = "Authentication:Google:ClientId";

        /// <summary>
        /// Google client secret.
        /// </summary>
        public const string AuthenticationGoogleClientSecret = "Authentication:Google:ClientSecret";

        /// <summary>
        /// Microsoft application ID.
        /// </summary>
        public const string AuthenticationMicrosoftApplicationId = "Authentication:Microsoft:ApplicationId";

        /// <summary>
        /// Microsoft application password.
        /// </summary>
        public const string AuthenticationMicrosoftPassword = "Authentication:Microsoft:Password";

        /// <summary>
        /// Twitter consumer key.
        /// </summary>
        public const string AuthenticationTwitterConsumerKey = "Authentication:Twitter:ConsumerKey";

        /// <summary>
        /// Twitter consumer secret.
        /// </summary>
        public const string AuthenticationTwitterConsumerSecret = "Authentication:Twitter:ConsumerSecret";

        /// <summary>
        /// Biotaxonomy database MongoDB connection string name.
        /// </summary>
        public const string BiotaxonomyDatabaseMongoDBConnectionStringName = "BiotaxonomyDatabaseMongoDB";

        /// <summary>
        /// Biotaxonomy MongoDB database name.
        /// </summary>
        public const string BiotaxonomyMongoDBDatabaseName = "DatabaseNames:BiotaxonomyMongoDB";

        /// <summary>
        /// Biorepositories database MongoDB connection string name.
        /// </summary>
        public const string BiorepositoriesDatabaseMongoDBConnectionStringName = "BiorepositoriesDatabaseMongoDB";

        /// <summary>
        /// Biorepositories MongoDB database name.
        /// </summary>
        public const string BiorepositoriesMongoDBDatabaseName = "DatabaseNames:BiorepositoriesMongoDB";

        /// <summary>
        /// Codes remove non-code nodes XSL file path.
        /// </summary>
        public const string CodesRemoveNonCodeNodesXslFilePath = "StaticFiles:CodesRemoveNonCodeNodesXslFilePath";

        /// <summary>
        /// DefaultConnection connection string name.
        /// </summary>
        public const string DefaultConnectionConnectionStringName = "DefaultConnection";

        /// <summary>
        /// Documents database MongoDB connection string name.
        /// </summary>
        public const string DocumentsDatabaseMongoDBConnectionStringName = "DocumentsDatabaseMongoDB";

        /// <summary>
        /// Documents MongoDB database name.
        /// </summary>
        public const string DocumentsMongoDBDatabaseName = "DatabaseNames:DocumentsMongoDB";

        /// <summary>
        /// External links XSL file path.
        /// </summary>
        public const string ExternalLinksXslFilePath = "StaticFiles:ExternalLinksXslFilePath";

        /// <summary>
        /// Catalogue of Life webservice base address.
        /// </summary>
        public const string ExternalServicesCatalogueOfLifeWebserviceBaseAddress = "ExternalServices:CatalogueOfLife:WebserviceBaseAddress";

        /// <summary>
        /// GBIF API v0.9 base address.
        /// </summary>
        public const string ExternalServicesGbifApi09BaseAddress = "ExternalServices:GBIF:Api09BaseAddress";

        /// <summary>
        /// Files database MongoDB connection string name.
        /// </summary>
        public const string FilesDatabaseMongoDBConnectionStringName = "FilesDatabaseMongoDB";

        /// <summary>
        /// Files MongoDB database name.
        /// </summary>
        public const string FilesMongoDBDatabaseName = "DatabaseNames:FilesMongoDB";

        /// <summary>
        /// Files root directory.
        /// </summary>
        public const string FilesRootDirectory = "Files:RootDirectory";

        /// <summary>
        /// Format HTML-to-XML XSL file path.
        /// </summary>
        public const string FormatHtmlToXmlXslFilePath = "StaticFiles:FormatHtmlToXmlXslFilePath";

        /// <summary>
        /// Format NLM to system XSL file path.
        /// </summary>
        public const string FormatNlmToSystemXslFilePath = "StaticFiles:FormatNlmToSystemXslFilePath";

        /// <summary>
        /// Format system to NLM XSL file path.
        /// </summary>
        public const string FormatSystemToNlmXslFilePath = "StaticFiles:FormatSystemToNlmXslFilePath";

        /// <summary>
        /// Format taxon treatments XSL file path.
        /// </summary>
        public const string FormatTaxonTreatmentsXslFilePath = "StaticFiles:FormatTaxonTreatmentsXslFilePath";

        /// <summary>
        /// Format XML-to-HTML XSL file path.
        /// </summary>
        public const string FormatXmlToHtmlXslFilePath = "StaticFiles:FormatXmlToHtmlXslFilePath";

        /// <summary>
        /// Gavin-Laurens XSL file path.
        /// </summary>
        public const string GavinLaurensXslFilePath = "StaticFiles:GavinLaurensXslFilePath";

        /// <summary>
        /// History database MongoDB connection string name.
        /// </summary>
        public const string HistoryDatabaseMongoDBConnectionStringName = "HistoryDatabaseMongoDB";

        /// <summary>
        /// History MongoDB database name.
        /// </summary>
        public const string HistoryMongoDBDatabaseName = "DatabaseNames:HistoryMongoDB";

        /// <summary>
        /// HttpServer:Endpoints section name.
        /// </summary>
        public const string HttpServerEndpointsSectionName = "HttpServer:Endpoints";

        /// <summary>
        /// Kestrel section name.
        /// </summary>
        public const string KestrelSectionName = "Kestrel";

        /// <summary>
        /// Layout database MongoDB connection string name.
        /// </summary>
        public const string LayoutDatabaseMongoDBConnectionStringName = "LayoutDatabaseMongoDB";

        /// <summary>
        /// Layout MongoDB database name.
        /// </summary>
        public const string LayoutMongoDBDatabaseName = "DatabaseNames:LayoutMongoDB";

        /// <summary>
        /// Logging section name.
        /// </summary>
        public const string LoggingSectionName = "Logging";

        /// <summary>
        /// Message queue exchange name.
        /// </summary>
        public const string MessageQueueExchangeName = "MessageQueue:ExchangeName";

        /// <summary>
        /// Message queue host name.
        /// </summary>
        public const string MessageQueueHostName = "MessageQueue:HostName";

        /// <summary>
        /// Message queue password.
        /// </summary>
        public const string MessageQueuePassword = "MessageQueue:Password";

        /// <summary>
        /// Message queue port.
        /// </summary>
        public const string MessageQueuePort = "MessageQueue:Port";

        /// <summary>
        /// Message queue name.
        /// </summary>
        public const string MessageQueueQueueName = "MessageQueue:QueueName";

        /// <summary>
        /// Message queue user name.
        /// </summary>
        public const string MessageQueueUserName = "MessageQueue:UserName";

        /// <summary>
        /// Message queue virtual host.
        /// </summary>
        public const string MessageQueueVirtualHost = "MessageQueue:VirtualHost";

        /// <summary>
        /// NLM initial format XSL file path.
        /// </summary>
        public const string NlmInitialFormatXslFilePath = "StaticFiles:NlmInitialFormatXslFilePath";

        /// <summary>
        /// Parse treatment meta with internal information XSL file path.
        /// </summary>
        public const string ParseTreatmentMetaWithInternalInformationXslFilePath = "StaticFiles:ParseTreatmentMetaWithInternalInformationXslFilePath";

        /// <summary>
        /// References get references XSL file path.
        /// </summary>
        public const string ReferencesGetReferencesXslFilePath = "StaticFiles:ReferencesGetReferencesXslFilePath";

        /// <summary>
        /// References tag template XSL file path.
        /// </summary>
        public const string ReferencesTagTemplateXslFilePath = "StaticFiles:ReferencesTagTemplateXslFilePath";

        /// <summary>
        /// Remove taxon name parts XSL file path.
        /// </summary>
        public const string RemoveTaxonNamePartsXslFilePath = "StaticFiles:RemoveTaxonNamePartsXslFilePath";

        /// <summary>
        /// System initial format XSL file path.
        /// </summary>
        public const string SystemInitialFormatXslFilePath = "StaticFiles:SystemInitialFormatXslFilePath";

        /// <summary>
        /// Taxon treatment extract materials XSL file path.
        /// </summary>
        public const string TaxonTreatmentExtractMaterialsXslFilePath = "StaticFiles:TaxonTreatmentExtractMaterialsXslFilePath";

        /// <summary>
        /// Text content XSL file path.
        /// </summary>
        public const string TextContentXslFilePath = "StaticFiles:TextContentXslFilePath";

        /// <summary>
        /// Users database MSSQL connection string name.
        /// </summary>
        public const string UsersDatabaseMSSQLConnectionStringName = "UsersDatabaseMSSQL";

        /// <summary>
        /// Users database SQLite connection string name.
        /// </summary>
        public const string UsersDatabaseSQLiteConnectionStringName = "UsersDatabaseSQLite";

        /// <summary>
        /// Users database type.
        /// </summary>
        public const string UsersDatabaseType = "DatabaseTypes:UsersDatabase";

        /// <summary>
        /// ZooBank registration NLM XSL file path.
        /// </summary>
        public const string ZooBankRegistrationNlmXslFilePath = "StaticFiles:ZooBankRegistrationNlmXslFilePath";
    }
}
