// <copyright file="AppSettings.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Constants.Configuration
{
    using System.Configuration;

    /// <summary>
    /// Application settings.
    /// </summary>
    public static class AppSettings
    {
        /// <summary>
        /// Abbreviations XQuery file name.
        /// </summary>
        public static readonly string AbbreviationsXQueryFileName = ConfigurationManager.AppSettings[AppSettingsKeys.AbbreviationsXQueryFileName];

        /// <summary>
        /// AppData directory name.
        /// </summary>
        public static readonly string AppDataDirectoryName = ConfigurationManager.AppSettings[AppSettingsKeys.AppDataDirectoryName];

        /// <summary>
        /// Biorepositories Mongo connection.
        /// </summary>
        public static readonly string BiorepositoriesMongoConnection = ConfigurationManager.AppSettings[AppSettingsKeys.BiorepositoriesMongoConnection];

        /// <summary>
        /// Biorepositories Mongo database name.
        /// </summary>
        public static readonly string BiorepositoriesMongoDatabaseName = ConfigurationManager.AppSettings[AppSettingsKeys.BiorepositoriesMongoDatabaseName];

        /// <summary>
        /// Biorepositories seed CSV data files directory name.
        /// </summary>
        public static readonly string BiorepositoriesSeedCsvDataFilesDirectoryName = ConfigurationManager.AppSettings[AppSettingsKeys.BiorepositoriesSeedCsvDataFilesDirectoryName];

        /// <summary>
        /// Biotaxonomy black list XML file name.
        /// </summary>
        public static readonly string BiotaxonomyBlackListXmlFileName = ConfigurationManager.AppSettings[AppSettingsKeys.BiotaxonomyBlackListXmlFileName];

        /// <summary>
        /// Biotaxonomy Mongo connection.
        /// </summary>
        public static readonly string BiotaxonomyMongoConnection = ConfigurationManager.AppSettings[AppSettingsKeys.BiotaxonomyMongoConnection];

        /// <summary>
        /// Biotaxonomy Mongo database name.
        /// </summary>
        public static readonly string BiotaxonomyMongoDatabaseName = ConfigurationManager.AppSettings[AppSettingsKeys.BiotaxonomyMongoDatabaseName];

        /// <summary>
        /// Biotaxonomy rank list XML file name.
        /// </summary>
        public static readonly string BiotaxonomyRankListXmlFileName = ConfigurationManager.AppSettings[AppSettingsKeys.BiotaxonomyRankListXmlFileName];

        /// <summary>
        /// Cache Mongo connection.
        /// </summary>
        public static readonly string CacheMongoConnection = ConfigurationManager.AppSettings[AppSettingsKeys.CacheMongoConnection];

        /// <summary>
        /// Cache Mongo database name.
        /// </summary>
        public static readonly string CacheMongoDatabaseName = ConfigurationManager.AppSettings[AppSettingsKeys.CacheMongoDatabaseName];

        /// <summary>
        /// Client settings provider.
        /// </summary>
        public static readonly string ClientSettingsProvider = ConfigurationManager.AppSettings[AppSettingsKeys.ClientSettingsProvider];

        /// <summary>
        /// Codes remove non-code nodes XSL file name.
        /// </summary>
        public static readonly string CodesRemoveNonCodeNodesXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.CodesRemoveNonCodeNodesXslFileName];

        /// <summary>
        /// Continent codes seed file name.
        /// </summary>
        public static readonly string ContinentCodesSeedFileName = ConfigurationManager.AppSettings[AppSettingsKeys.ContinentCodesSeedFileName];

        /// <summary>
        /// Country codes seed file name.
        /// </summary>
        public static readonly string CountryCodesSeedFileName = ConfigurationManager.AppSettings[AppSettingsKeys.CountryCodesSeedFileName];

        /// <summary>
        /// Documents Mongo connection.
        /// </summary>
        public static readonly string DocumentsMongoConnection = ConfigurationManager.AppSettings[AppSettingsKeys.DocumentsMongoConnection];

        /// <summary>
        /// Documents Mongo database name.
        /// </summary>
        public static readonly string DocumentsMongoDatabaseName = ConfigurationManager.AppSettings[AppSettingsKeys.DocumentsMongoDatabaseName];

        /// <summary>
        /// Environments entities file name.
        /// </summary>
        public static readonly string EnvironmentsEntitiesFileName = ConfigurationManager.AppSettings[AppSettingsKeys.EnvironmentsEntitiesFileName];

        /// <summary>
        /// Environments global file name.
        /// </summary>
        public static readonly string EnvironmentsGlobalFileName = ConfigurationManager.AppSettings[AppSettingsKeys.EnvironmentsGlobalFileName];

        /// <summary>
        /// Environments groups file name.
        /// </summary>
        public static readonly string EnvironmentsGroupsFileName = ConfigurationManager.AppSettings[AppSettingsKeys.EnvironmentsGroupsFileName];

        /// <summary>
        /// Environments names file name.
        /// </summary>
        public static readonly string EnvironmentsNamesFileName = ConfigurationManager.AppSettings[AppSettingsKeys.EnvironmentsNamesFileName];

        /// <summary>
        /// External links XSL file name.
        /// </summary>
        public static readonly string ExternalLinksXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.ExternalLinksXslFileName];

        /// <summary>
        /// Facebook App ID.
        /// </summary>
        public static readonly string FacebookAppId = ConfigurationManager.AppSettings[AppSettingsKeys.FacebookAppId];

        /// <summary>
        /// Facebook App secret.
        /// </summary>
        public static readonly string FacebookAppSecret = ConfigurationManager.AppSettings[AppSettingsKeys.FacebookAppSecret];

        /// <summary>
        /// Format HTML-to-XML XSL file name.
        /// </summary>
        public static readonly string FormatHtmlToXmlXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.FormatHtmlToXmlXslFileName];

        /// <summary>
        /// Format NLM-to-system XSL file name.
        /// </summary>
        public static readonly string FormatNlmToSystemXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.FormatNlmToSystemXslFileName];

        /// <summary>
        /// Format system-to-NLM XSL file name.
        /// </summary>
        public static readonly string FormatSystemToNlmXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.FormatSystemToNlmXslFileName];

        /// <summary>
        /// Format taxon treatments XSL file name.
        /// </summary>
        public static readonly string FormatTaxonTreatmentsXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.FormatTaxonTreatmentsXslFileName];

        /// <summary>
        /// Format XML-to-HTML XSL file name.
        /// </summary>
        public static readonly string FormatXmlToHtmlXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.FormatXmlToHtmlXslFileName];

        /// <summary>
        /// Gavin-Laurens XSL file name.
        /// </summary>
        public static readonly string GavinLaurensXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.GavinLaurensXslFileName];

        /// <summary>
        /// Geo-epithets seed file name.
        /// </summary>
        public static readonly string GeoEpithetsSeedFileName = ConfigurationManager.AppSettings[AppSettingsKeys.GeoEpithetsSeedFileName];

        /// <summary>
        /// Geo-names seed file name.
        /// </summary>
        public static readonly string GeoNamesSeedFileName = ConfigurationManager.AppSettings[AppSettingsKeys.GeoNamesSeedFileName];

        /// <summary>
        /// Google client ID.
        /// </summary>
        public static readonly string GoogleClientId = ConfigurationManager.AppSettings[AppSettingsKeys.GoogleClientId];

        /// <summary>
        /// Google client secret.
        /// </summary>
        public static readonly string GoogleClientSecret = ConfigurationManager.AppSettings[AppSettingsKeys.GoogleClientSecret];

        /// <summary>
        /// Institutions seed file name.
        /// </summary>
        public static readonly string InstitutionsSeedFileName = ConfigurationManager.AppSettings[AppSettingsKeys.InstitutionsSeedFileName];

        /// <summary>
        /// Journals JSON files directory name.
        /// </summary>
        public static readonly string JournalsJsonFilesDirectoryName = ConfigurationManager.AppSettings[AppSettingsKeys.JournalsJsonFilesDirectoryName];

        /// <summary>
        /// Maximal time in minutes to wait the main thread.
        /// </summary>
        public static readonly string MaximalTimeInMinutesToWaitTheMainThread = ConfigurationManager.AppSettings[AppSettingsKeys.MaximalTimeInMinutesToWaitTheMainThread];

        /// <summary>
        /// Media type data JSON file name.
        /// </summary>
        public static readonly string MediaTypeDataJsonFileName = ConfigurationManager.AppSettings[AppSettingsKeys.MediaTypeDataJsonFileName];

        /// <summary>
        /// Mediatypes Mongo connection.
        /// </summary>
        public static readonly string MediatypesMongoConnection = ConfigurationManager.AppSettings[AppSettingsKeys.MediatypesMongoConnection];

        /// <summary>
        /// Mediatypes Mongo database name.
        /// </summary>
        public static readonly string MediatypesMongoDatabaseName = ConfigurationManager.AppSettings[AppSettingsKeys.MediatypesMongoDatabaseName];

        /// <summary>
        /// Microsoft client ID.
        /// </summary>
        public static readonly string MicrosoftClientId = ConfigurationManager.AppSettings[AppSettingsKeys.MicrosoftClientId];

        /// <summary>
        /// Microsoft client secret.
        /// </summary>
        public static readonly string MicrosoftClientSecret = ConfigurationManager.AppSettings[AppSettingsKeys.MicrosoftClientSecret];

        /// <summary>
        /// Morphological epithets file name.
        /// </summary>
        public static readonly string MorphologicalEpithetsFileName = ConfigurationManager.AppSettings[AppSettingsKeys.MorphologicalEpithetsFileName];

        /// <summary>
        /// NLM initial format XSL file name.
        /// </summary>
        public static readonly string NlmInitialFormatXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.NlmInitialFormatXslFileName];

        /// <summary>
        /// Parse treatment meta with internal information XSL file name.
        /// </summary>
        public static readonly string ParseTreatmentMetaWithInternalInformationXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.ParseTreatmentMetaWithInternalInformationXslFileName];

        /// <summary>
        /// Products seed file name.
        /// </summary>
        public static readonly string ProductsSeedFileName = ConfigurationManager.AppSettings[AppSettingsKeys.ProductsSeedFileName];

        /// <summary>
        /// Ranks data file name.
        /// </summary>
        public static readonly string RanksDataFileName = ConfigurationManager.AppSettings[AppSettingsKeys.RanksDataFileName];

        /// <summary>
        /// Redis connection.
        /// </summary>
        public static readonly string RedisConnection = ConfigurationManager.AppSettings[AppSettingsKeys.RedisConnection];

        /// <summary>
        /// References get references XSL file name.
        /// </summary>
        public static readonly string ReferencesGetReferencesXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.ReferencesGetReferencesXslFileName];

        /// <summary>
        /// References tag template XSL file name.
        /// </summary>
        public static readonly string ReferencesTagTemplateXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.ReferencesTagTemplateXslFileName];

        /// <summary>
        /// Remove taxon name parts XSL file name.
        /// </summary>
        public static readonly string RemoveTaxonNamePartsXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.RemoveTaxonNamePartsXslFileName];

        /// <summary>
        /// System initial format XSL file name.
        /// </summary>
        public static readonly string SystemInitialFormatXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.SystemInitialFormatXslFileName];

        /// <summary>
        /// Taxon treatment extract materials XSL file name.
        /// </summary>
        public static readonly string TaxonTreatmentExtractMaterialsXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.TaxonTreatmentExtractMaterialsXslFileName];

        /// <summary>
        /// TaxPub DTD path.
        /// </summary>
        public static readonly string TaxPubDtdPath = ConfigurationManager.AppSettings[AppSettingsKeys.TaxPubDtdPath];

        /// <summary>
        /// Text content XSL file name.
        /// </summary>
        public static readonly string TextContentXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.TextContentXslFileName];

        /// <summary>
        /// Twitter consumer key.
        /// </summary>
        public static readonly string TwitterConsumerKey = ConfigurationManager.AppSettings[AppSettingsKeys.TwitterConsumerKey];

        /// <summary>
        /// Twitter consumer secret.
        /// </summary>
        public static readonly string TwitterConsumerSecret = ConfigurationManager.AppSettings[AppSettingsKeys.TwitterConsumerSecret];

        /// <summary>
        /// Type statuses file name.
        /// </summary>
        public static readonly string TypeStatusesFileName = ConfigurationManager.AppSettings[AppSettingsKeys.TypeStatusesFileName];

        /// <summary>
        /// ZooBank registration NLM XSL file name.
        /// </summary>
        public static readonly string ZooBankRegistrationNlmXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.ZooBankRegistrationNlmXslFileName];

        /// <summary>
        /// BingMaps Key.
        /// </summary>
        public static readonly string BingMapsKey = ConfigurationManager.AppSettings[AppSettingsKeys.BingMapsKey];
    }
}
