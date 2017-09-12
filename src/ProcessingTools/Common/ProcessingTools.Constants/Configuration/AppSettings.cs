// <copyright file="AppSettings.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Constants.Configuration
{
    using System.Configuration;

    /// <summary>
    /// AppSettings
    /// </summary>
    public static class AppSettings
    {
        /// <summary>
        /// AbbreviationsXQueryFileName
        /// </summary>
        public static readonly string AbbreviationsXQueryFileName = ConfigurationManager.AppSettings[AppSettingsKeys.AbbreviationsXQueryFileName];

        /// <summary>
        /// AppDataDirectoryName
        /// </summary>
        public static readonly string AppDataDirectoryName = ConfigurationManager.AppSettings[AppSettingsKeys.AppDataDirectoryName];

        /// <summary>
        /// BiorepositoriesMongoConnection
        /// </summary>
        public static readonly string BiorepositoriesMongoConnection = ConfigurationManager.AppSettings[AppSettingsKeys.BiorepositoriesMongoConnection];

        /// <summary>
        /// BiorepositoriesMongoDabaseName
        /// </summary>
        public static readonly string BiorepositoriesMongoDabaseName = ConfigurationManager.AppSettings[AppSettingsKeys.BiorepositoriesMongoDabaseName];

        /// <summary>
        /// BiorepositoriesSeedCsvDataFilesDirectoryName
        /// </summary>
        public static readonly string BiorepositoriesSeedCsvDataFilesDirectoryName = ConfigurationManager.AppSettings[AppSettingsKeys.BiorepositoriesSeedCsvDataFilesDirectoryName];

        /// <summary>
        /// BiotaxonomyBlackListXmlFileName
        /// </summary>
        public static readonly string BiotaxonomyBlackListXmlFileName = ConfigurationManager.AppSettings[AppSettingsKeys.BiotaxonomyBlackListXmlFileName];

        /// <summary>
        /// BiotaxonomyMongoConnection
        /// </summary>
        public static readonly string BiotaxonomyMongoConnection = ConfigurationManager.AppSettings[AppSettingsKeys.BiotaxonomyMongoConnection];

        /// <summary>
        /// BiotaxonomyMongoDabaseName
        /// </summary>
        public static readonly string BiotaxonomyMongoDabaseName = ConfigurationManager.AppSettings[AppSettingsKeys.BiotaxonomyMongoDabaseName];

        /// <summary>
        /// BiotaxonomyRankListXmlFileName
        /// </summary>
        public static readonly string BiotaxonomyRankListXmlFileName = ConfigurationManager.AppSettings[AppSettingsKeys.BiotaxonomyRankListXmlFileName];

        /// <summary>
        /// CacheMongoConnection
        /// </summary>
        public static readonly string CacheMongoConnection = ConfigurationManager.AppSettings[AppSettingsKeys.CacheMongoConnection];

        /// <summary>
        /// CacheMongoDabaseName
        /// </summary>
        public static readonly string CacheMongoDabaseName = ConfigurationManager.AppSettings[AppSettingsKeys.CacheMongoDabaseName];

        /// <summary>
        /// ClientSettingsProvider
        /// </summary>
        public static readonly string ClientSettingsProvider = ConfigurationManager.AppSettings[AppSettingsKeys.ClientSettingsProvider];

        /// <summary>
        /// CodesRemoveNonCodeNodesXslFileName
        /// </summary>
        public static readonly string CodesRemoveNonCodeNodesXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.CodesRemoveNonCodeNodesXslFileName];

        /// <summary>
        /// ContinentsCodesSeedFileName
        /// </summary>
        public static readonly string ContinentsCodesSeedFileName = ConfigurationManager.AppSettings[AppSettingsKeys.ContinentsCodesSeedFileName];

        /// <summary>
        /// CountryCodesSeedFileName
        /// </summary>
        public static readonly string CountryCodesSeedFileName = ConfigurationManager.AppSettings[AppSettingsKeys.CountryCodesSeedFileName];

        /// <summary>
        /// DataFilesDirectoryName
        /// </summary>
        public static readonly string DataFilesDirectoryName = ConfigurationManager.AppSettings[AppSettingsKeys.DataFilesDirectoryName];

        /// <summary>
        /// DocumentsMongoConnection
        /// </summary>
        public static readonly string DocumentsMongoConnection = ConfigurationManager.AppSettings[AppSettingsKeys.DocumentsMongoConnection];

        /// <summary>
        /// DocumentsMongoDabaseName
        /// </summary>
        public static readonly string DocumentsMongoDabaseName = ConfigurationManager.AppSettings[AppSettingsKeys.DocumentsMongoDabaseName];

        /// <summary>
        /// EnvironmentsEntitiesFileName
        /// </summary>
        public static readonly string EnvironmentsEntitiesFileName = ConfigurationManager.AppSettings[AppSettingsKeys.EnvironmentsEntitiesFileName];

        /// <summary>
        /// EnvironmentsGlobalFileName
        /// </summary>
        public static readonly string EnvironmentsGlobalFileName = ConfigurationManager.AppSettings[AppSettingsKeys.EnvironmentsGlobalFileName];

        /// <summary>
        /// EnvironmentsGroupsFileName
        /// </summary>
        public static readonly string EnvironmentsGroupsFileName = ConfigurationManager.AppSettings[AppSettingsKeys.EnvironmentsGroupsFileName];

        /// <summary>
        /// EnvironmentsNamesFileName
        /// </summary>
        public static readonly string EnvironmentsNamesFileName = ConfigurationManager.AppSettings[AppSettingsKeys.EnvironmentsNamesFileName];

        /// <summary>
        /// ExternalLinksXslFileName
        /// </summary>
        public static readonly string ExternalLinksXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.ExternalLinksXslFileName];

        /// <summary>
        /// FacebookAppId
        /// </summary>
        public static readonly string FacebookAppId = ConfigurationManager.AppSettings[AppSettingsKeys.FacebookAppId];

        /// <summary>
        /// FacebookAppSecret
        /// </summary>
        public static readonly string FacebookAppSecret = ConfigurationManager.AppSettings[AppSettingsKeys.FacebookAppSecret];

        /// <summary>
        /// FormatHtmlToXmlXslFileName
        /// </summary>
        public static readonly string FormatHtmlToXmlXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.FormatHtmlToXmlXslFileName];

        /// <summary>
        /// FormatNlmToSystemXslFileName
        /// </summary>
        public static readonly string FormatNlmToSystemXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.FormatNlmToSystemXslFileName];

        /// <summary>
        /// FormatSystemToNlmXslFileName
        /// </summary>
        public static readonly string FormatSystemToNlmXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.FormatSystemToNlmXslFileName];

        /// <summary>
        /// FormatTaxonTreatmentsXslFileName
        /// </summary>
        public static readonly string FormatTaxonTreatmentsXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.FormatTaxonTreatmentsXslFileName];

        /// <summary>
        /// FormatXmlToHtmlXslFileName
        /// </summary>
        public static readonly string FormatXmlToHtmlXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.FormatXmlToHtmlXslFileName];

        /// <summary>
        /// GavinLaurensXslFileName
        /// </summary>
        public static readonly string GavinLaurensXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.GavinLaurensXslFileName];

        /// <summary>
        /// GeoEpithetsSeedFileName
        /// </summary>
        public static readonly string GeoEpithetsSeedFileName = ConfigurationManager.AppSettings[AppSettingsKeys.GeoEpithetsSeedFileName];

        /// <summary>
        /// GeoNamesSeedFileName
        /// </summary>
        public static readonly string GeoNamesSeedFileName = ConfigurationManager.AppSettings[AppSettingsKeys.GeoNamesSeedFileName];

        /// <summary>
        /// GoogleClientId
        /// </summary>
        public static readonly string GoogleClientId = ConfigurationManager.AppSettings[AppSettingsKeys.GoogleClientId];

        /// <summary>
        /// GoogleClientSecret
        /// </summary>
        public static readonly string GoogleClientSecret = ConfigurationManager.AppSettings[AppSettingsKeys.GoogleClientSecret];

        /// <summary>
        /// InstitutionsSeedFileName
        /// </summary>
        public static readonly string InstitutionsSeedFileName = ConfigurationManager.AppSettings[AppSettingsKeys.InstitutionsSeedFileName];

        /// <summary>
        /// JournalsJsonFilesDirectoryName
        /// </summary>
        public static readonly string JournalsJsonFilesDirectoryName = ConfigurationManager.AppSettings[AppSettingsKeys.JournalsJsonFilesDirectoryName];

        /// <summary>
        /// MaximalTimeInMinutesToWaitTheMainThread
        /// </summary>
        public static readonly string MaximalTimeInMinutesToWaitTheMainThread = ConfigurationManager.AppSettings[AppSettingsKeys.MaximalTimeInMinutesToWaitTheMainThread];

        /// <summary>
        /// MediaTypeDataJsonFileName
        /// </summary>
        public static readonly string MediaTypeDataJsonFileName = ConfigurationManager.AppSettings[AppSettingsKeys.MediaTypeDataJsonFileName];

        /// <summary>
        /// MediatypesMongoConnection
        /// </summary>
        public static readonly string MediatypesMongoConnection = ConfigurationManager.AppSettings[AppSettingsKeys.MediatypesMongoConnection];

        /// <summary>
        /// MediatypesMongoDabaseName
        /// </summary>
        public static readonly string MediatypesMongoDabaseName = ConfigurationManager.AppSettings[AppSettingsKeys.MediatypesMongoDabaseName];

        /// <summary>
        /// MicrosoftClientId
        /// </summary>
        public static readonly string MicrosoftClientId = ConfigurationManager.AppSettings[AppSettingsKeys.MicrosoftClientId];

        /// <summary>
        /// MicrosoftClientSecret
        /// </summary>
        public static readonly string MicrosoftClientSecret = ConfigurationManager.AppSettings[AppSettingsKeys.MicrosoftClientSecret];

        /// <summary>
        /// MorphologicalEpithetsFileName
        /// </summary>
        public static readonly string MorphologicalEpithetsFileName = ConfigurationManager.AppSettings[AppSettingsKeys.MorphologicalEpithetsFileName];

        /// <summary>
        /// NlmInitialFormatXslFileName
        /// </summary>
        public static readonly string NlmInitialFormatXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.NlmInitialFormatXslFileName];

        /// <summary>
        /// ParseTreatmentMetaWithInternalInformationXslFileName
        /// </summary>
        public static readonly string ParseTreatmentMetaWithInternalInformationXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.ParseTreatmentMetaWithInternalInformationXslFileName];

        /// <summary>
        /// ProductsSeedFileName
        /// </summary>
        public static readonly string ProductsSeedFileName = ConfigurationManager.AppSettings[AppSettingsKeys.ProductsSeedFileName];

        /// <summary>
        /// RanksDataFileName
        /// </summary>
        public static readonly string RanksDataFileName = ConfigurationManager.AppSettings[AppSettingsKeys.RanksDataFileName];

        /// <summary>
        /// RedisConnection
        /// </summary>
        public static readonly string RedisConnection = ConfigurationManager.AppSettings[AppSettingsKeys.RedisConnection];

        /// <summary>
        /// ReferencesGetReferencesXslFileName
        /// </summary>
        public static readonly string ReferencesGetReferencesXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.ReferencesGetReferencesXslFileName];

        /// <summary>
        /// ReferencesTagTemplateXslFileName
        /// </summary>
        public static readonly string ReferencesTagTemplateXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.ReferencesTagTemplateXslFileName];

        /// <summary>
        /// RemoveTaxonNamePartsXslFileName
        /// </summary>
        public static readonly string RemoveTaxonNamePartsXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.RemoveTaxonNamePartsXslFileName];

        /// <summary>
        /// SystemInitialFormatXslFileName
        /// </summary>
        public static readonly string SystemInitialFormatXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.SystemInitialFormatXslFileName];

        /// <summary>
        /// TaxonTreatmentExtractMaterialsXslFileName
        /// </summary>
        public static readonly string TaxonTreatmentExtractMaterialsXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.TaxonTreatmentExtractMaterialsXslFileName];

        /// <summary>
        /// TaxPubDtdPath
        /// </summary>
        public static readonly string TaxPubDtdPath = ConfigurationManager.AppSettings[AppSettingsKeys.TaxPubDtdPath];

        /// <summary>
        /// TextContentXslFileName
        /// </summary>
        public static readonly string TextContentXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.TextContentXslFileName];

        /// <summary>
        /// TwitterConsumerKey
        /// </summary>
        public static readonly string TwitterConsumerKey = ConfigurationManager.AppSettings[AppSettingsKeys.TwitterConsumerKey];

        /// <summary>
        /// TwitterConsumerSecret
        /// </summary>
        public static readonly string TwitterConsumerSecret = ConfigurationManager.AppSettings[AppSettingsKeys.TwitterConsumerSecret];

        /// <summary>
        /// TypeStatusesFileName
        /// </summary>
        public static readonly string TypeStatusesFileName = ConfigurationManager.AppSettings[AppSettingsKeys.TypeStatusesFileName];

        /// <summary>
        /// ZooBankRegistrationNlmXslFileName
        /// </summary>
        public static readonly string ZooBankRegistrationNlmXslFileName = ConfigurationManager.AppSettings[AppSettingsKeys.ZooBankRegistrationNlmXslFileName];

        /// <summary>
        /// BlackListSampleFileName
        /// </summary>
        public static readonly string BlackListSampleFileName = ConfigurationManager.AppSettings[AppSettingsKeys.BlackListSampleFileName];

        /// <summary>
        /// RankListSampleFileName
        /// </summary>
        public static readonly string RankListSampleFileName = ConfigurationManager.AppSettings[AppSettingsKeys.RankListSampleFileName];

        /// <summary>
        /// SampleFiles
        /// </summary>
        public static readonly string SampleFiles = ConfigurationManager.AppSettings[AppSettingsKeys.SampleFiles];
    }
}
