namespace ProcessingTools.Configurator.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ConfigTests
    {
        private const string SampleFilePath = "/tmp/path";
        private Config config;

        [SetUp]
        public void Init()
        {
            this.config = new Config();
        }

        [Test]
        public void Config_ValidChangesOfBlackListCleanXslPathProperty_SchouldBePersistent()
        {
            this.config.blackListCleanXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.blackListCleanXslPath,
                "1. BlackListCleanXslPath should match SampleFilePath.");

            this.config.blackListCleanXslPath = null;
            Assert.IsNull(
                this.config.blackListCleanXslPath,
                "2. BlackListCleanXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfBlackListXmlFilePathProperty_SchouldBePersistent()
        {
            this.config.blackListXmlFilePath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.blackListXmlFilePath,
                "1. BlackListXmlFilePath should match SampleFilePath.");

            this.config.blackListXmlFilePath = null;
            Assert.IsNull(
                this.config.blackListXmlFilePath,
                "2. BlackListXmlFilePath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfCodesRemoveNonCodeNodesProperty_SchouldBePersistent()
        {
            this.config.codesRemoveNonCodeNodes = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.codesRemoveNonCodeNodes,
                "1. CodesRemoveNonCodeNodes should match SampleFilePath.");

            this.config.codesRemoveNonCodeNodes = null;
            Assert.IsNull(
                this.config.codesRemoveNonCodeNodes,
                "2. CodesRemoveNonCodeNodes should be null.");
        }

        [Test]
        public void Config_ValidChangesOfEnvironmentsDataSourceStringProperty_SchouldBePersistent()
        {
            this.config.environmentsDataSourceString = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.environmentsDataSourceString,
                "1. EnvironmentsDataSourceString should match SampleFilePath.");

            this.config.environmentsDataSourceString = null;
            Assert.IsNull(
                this.config.environmentsDataSourceString,
                "2. EnvironmentsDataSourceString should be null.");
        }

        [Test]
        public void Config_ValidChangesOfEnvoTermsWebServiceTransformXslPathProperty_SchouldBePersistent()
        {
            this.config.envoTermsWebServiceTransformXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.envoTermsWebServiceTransformXslPath,
                "1. EnvoTermsWebServiceTransformXslPath should match SampleFilePath.");

            this.config.envoTermsWebServiceTransformXslPath = null;
            Assert.IsNull(
                this.config.envoTermsWebServiceTransformXslPath,
                "2. EnvoTermsWebServiceTransformXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfFloraDistrinctTaxaXslPathProperty_SchouldBePersistent()
        {
            this.config.floraDistrinctTaxaXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.floraDistrinctTaxaXslPath,
                "1. FloraDistrinctTaxaXslPath should match SampleFilePath.");

            this.config.floraDistrinctTaxaXslPath = null;
            Assert.IsNull(
                this.config.floraDistrinctTaxaXslPath,
                "2. FloraDistrinctTaxaXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfFloraExtractedTaxaListPathProperty_SchouldBePersistent()
        {
            this.config.floraExtractedTaxaListPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.floraExtractedTaxaListPath,
                "1. FloraExtractedTaxaListPath should match SampleFilePath.");

            this.config.floraExtractedTaxaListPath = null;
            Assert.IsNull(
                this.config.floraExtractedTaxaListPath,
                "2. FloraExtractedTaxaListPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfFloraExtractTaxaPartsOutputPathProperty_SchouldBePersistent()
        {
            this.config.floraExtractTaxaPartsOutputPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.floraExtractTaxaPartsOutputPath,
                "1. FloraExtractTaxaPartsOutputPath should match SampleFilePath.");

            this.config.floraExtractTaxaPartsOutputPath = null;
            Assert.IsNull(
                this.config.floraExtractTaxaPartsOutputPath,
                "2. FloraExtractTaxaPartsOutputPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfFloraExtractTaxaPartsXslPathProperty_SchouldBePersistent()
        {
            this.config.floraExtractTaxaPartsXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.floraExtractTaxaPartsXslPath,
                "1. FloraExtractTaxaPartsXslPath should match SampleFilePath.");

            this.config.floraExtractTaxaPartsXslPath = null;
            Assert.IsNull(
                this.config.floraExtractTaxaPartsXslPath,
                "2. FloraExtractTaxaPartsXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfFloraExtractTaxaXslPathProperty_SchouldBePersistent()
        {
            this.config.floraExtractTaxaXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.floraExtractTaxaXslPath,
                "1. FloraExtractTaxaXslPath should match SampleFilePath.");

            this.config.floraExtractTaxaXslPath = null;
            Assert.IsNull(
                this.config.floraExtractTaxaXslPath,
                "2. FloraExtractTaxaXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfFloraGenerateTemplatesXslPathProperty_SchouldBePersistent()
        {
            this.config.floraGenerateTemplatesXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.floraGenerateTemplatesXslPath,
                "1. FloraGenerateTemplatesXslPath should match SampleFilePath.");

            this.config.floraGenerateTemplatesXslPath = null;
            Assert.IsNull(
                this.config.floraGenerateTemplatesXslPath,
                "2. FloraGenerateTemplatesXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfFloraTemplatesOutputXmlPathProperty_SchouldBePersistent()
        {
            this.config.floraTemplatesOutputXmlPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.floraTemplatesOutputXmlPath,
                "1. FloraTemplatesOutputXmlPath should match SampleFilePath.");

            this.config.floraTemplatesOutputXmlPath = null;
            Assert.IsNull(
                this.config.floraTemplatesOutputXmlPath,
                "2. FloraTemplatesOutputXmlPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfFormatXslNlmToSystemProperty_SchouldBePersistent()
        {
            this.config.formatXslNlmToSystem = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.formatXslNlmToSystem,
                "1. FormatXslNlmToSystem should match SampleFilePath.");

            this.config.formatXslNlmToSystem = null;
            Assert.IsNull(
                this.config.formatXslNlmToSystem,
                "2. FormatXslNlmToSystem should be null.");
        }

        [Test]
        public void Config_ValidChangesOfFormatXslSystemToNlmProperty_SchouldBePersistent()
        {
            this.config.formatXslSystemToNlm = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.formatXslSystemToNlm,
                "1. FormatXslSystemToNlm should match SampleFilePath.");

            this.config.formatXslSystemToNlm = null;
            Assert.IsNull(
                this.config.formatXslSystemToNlm,
                "2. FormatXslSystemToNlm should be null.");
        }

        [Test]
        public void Config_ValidChangesOfMainDictionaryDataSourceStringProperty_SchouldBePersistent()
        {
            this.config.mainDictionaryDataSourceString = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.mainDictionaryDataSourceString,
                "1. MainDictionaryDataSourceString should match SampleFilePath.");

            this.config.mainDictionaryDataSourceString = null;
            Assert.IsNull(
                this.config.mainDictionaryDataSourceString,
                "2. MainDictionaryDataSourceString should be null.");
        }

        [Test]
        public void Config_ValidChangesOfNlmInitialFormatXslPathProperty_SchouldBePersistent()
        {
            this.config.nlmInitialFormatXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.nlmInitialFormatXslPath,
                "1. NlmInitialFormatXslPath should match SampleFilePath.");

            this.config.nlmInitialFormatXslPath = null;
            Assert.IsNull(
                this.config.nlmInitialFormatXslPath,
                "2. NlmInitialFormatXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfRankListCleanXslPathProperty_SchouldBePersistent()
        {
            this.config.rankListCleanXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.rankListCleanXslPath,
                "1. RankListCleanXslPath should match SampleFilePath.");

            this.config.rankListCleanXslPath = null;
            Assert.IsNull(
                this.config.rankListCleanXslPath,
                "2. RankListCleanXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfRankListXmlFilePathProperty_SchouldBePersistent()
        {
            this.config.rankListXmlFilePath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.rankListXmlFilePath,
                "1. RankListXmlFilePath should match SampleFilePath.");

            this.config.rankListXmlFilePath = null;
            Assert.IsNull(
                this.config.rankListXmlFilePath,
                "2. RankListXmlFilePath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfReferencesGetReferencesXmlPathProperty_SchouldBePersistent()
        {
            this.config.referencesGetReferencesXmlPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.referencesGetReferencesXmlPath,
                "1. ReferencesGetReferencesXmlPath should match SampleFilePath.");

            this.config.referencesGetReferencesXmlPath = null;
            Assert.IsNull(
                this.config.referencesGetReferencesXmlPath,
                "2. ReferencesGetReferencesXmlPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfReferencesGetReferencesXslPathProperty_SchouldBePersistent()
        {
            this.config.referencesGetReferencesXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.referencesGetReferencesXslPath,
                "1. ReferencesGetReferencesXslPath should match SampleFilePath.");

            this.config.referencesGetReferencesXslPath = null;
            Assert.IsNull(
                this.config.referencesGetReferencesXslPath,
                "2. ReferencesGetReferencesXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfReferencesSortReferencesXslPathProperty_SchouldBePersistent()
        {
            this.config.referencesSortReferencesXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.referencesSortReferencesXslPath,
                "1. ReferencesSortReferencesXslPath should match SampleFilePath.");

            this.config.referencesSortReferencesXslPath = null;
            Assert.IsNull(
                this.config.referencesSortReferencesXslPath,
                "2. ReferencesSortReferencesXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfReferencesTagTemplateXmlPathProperty_SchouldBePersistent()
        {
            this.config.referencesTagTemplateXmlPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.referencesTagTemplateXmlPath,
                "1. ReferencesTagTemplateXmlPath should match SampleFilePath.");

            this.config.referencesTagTemplateXmlPath = null;
            Assert.IsNull(
                this.config.referencesTagTemplateXmlPath,
                "2. ReferencesTagTemplateXmlPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfReferencesTagTemplateXslPathProperty_SchouldBePersistent()
        {
            this.config.referencesTagTemplateXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.referencesTagTemplateXslPath,
                "1. ReferencesTagTemplateXslPath should match SampleFilePath.");

            this.config.referencesTagTemplateXslPath = null;
            Assert.IsNull(
                this.config.referencesTagTemplateXslPath,
                "2. ReferencesTagTemplateXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfSystemInitialFormatXslPathProperty_SchouldBePersistent()
        {
            this.config.systemInitialFormatXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.systemInitialFormatXslPath,
                "1. SystemInitialFormatXslPath should match SampleFilePath.");

            this.config.systemInitialFormatXslPath = null;
            Assert.IsNull(
                this.config.systemInitialFormatXslPath,
                "2. SystemInitialFormatXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfTempDirectoryPathProperty_SchouldBePersistent()
        {
            this.config.tempDirectoryPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.tempDirectoryPath,
                "1. TempDirectoryPath should match SampleFilePath.");

            this.config.tempDirectoryPath = null;
            Assert.IsNull(
                this.config.tempDirectoryPath,
                "2. TempDirectoryPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfTextContentXslFileNameProperty_SchouldBePersistent()
        {
            this.config.textContentXslFileName = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.textContentXslFileName,
                "1. TextContentXslFileName should match SampleFilePath.");

            this.config.textContentXslFileName = null;
            Assert.IsNull(
                this.config.textContentXslFileName,
                "2. TextContentXslFileName should be null.");
        }

        [Test]
        public void Config_ValidChangesOfWhiteListCleanXslPathProperty_SchouldBePersistent()
        {
            this.config.whiteListCleanXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.whiteListCleanXslPath,
                "1. WhiteListCleanXslPath should match SampleFilePath.");

            this.config.whiteListCleanXslPath = null;
            Assert.IsNull(
                this.config.whiteListCleanXslPath,
                "2. WhiteListCleanXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfWhiteListXmlFilePathProperty_SchouldBePersistent()
        {
            this.config.whiteListXmlFilePath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.whiteListXmlFilePath,
                "1. WhiteListXmlFilePath should match SampleFilePath.");

            this.config.whiteListXmlFilePath = null;
            Assert.IsNull(
                this.config.whiteListXmlFilePath,
                "2. WhiteListXmlFilePath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfZoobankNlmXslPathProperty_SchouldBePersistent()
        {
            this.config.zoobankNlmXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.zoobankNlmXslPath,
                "1. ZoobankNlmXslPath should match SampleFilePath.");

            this.config.zoobankNlmXslPath = null;
            Assert.IsNull(
                this.config.zoobankNlmXslPath,
                "2. ZoobankNlmXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfEnvoResponseOutputXmlFileNameProperty_SchouldBePersistent()
        {
            this.config.EnvoResponseOutputXmlFileName = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.EnvoResponseOutputXmlFileName,
                "1. EnvoResponseOutputXmlFileName should match SampleFilePath.");

            this.config.EnvoResponseOutputXmlFileName = null;
            Assert.IsNull(
                this.config.EnvoResponseOutputXmlFileName,
                "2. EnvoResponseOutputXmlFileName should be null.");
        }

        [Test]
        public void Config_ValidChangesOfGnrOutputFileNameProperty_SchouldBePersistent()
        {
            this.config.GnrOutputFileName = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.GnrOutputFileName,
                "1. GnrOutputFileName should match SampleFilePath.");

            this.config.GnrOutputFileName = null;
            Assert.IsNull(
                this.config.GnrOutputFileName,
                "2. GnrOutputFileName should be null.");
        }

        [Test]
        public void Config_ArticleSchemaTypePropertyInNewInstance_ShouldBeSystem()
        {
            Assert.AreEqual(SchemaType.System, this.config.ArticleSchemaType, "Default value of the ArticleSchemaType Property should be System.");
        }

        [Test]
        public void Config_FirstSetValueToSystemOfArticleSchemaTypePropertyBeforeGet_ShouldSetTheValueOfArticleSchemaTypePropertyToSystem()
        {
            this.config.ArticleSchemaType = SchemaType.System;
            Assert.AreEqual(SchemaType.System, this.config.ArticleSchemaType, "ArticleSchemaType should be System.");
        }

        [Test]
        public void Config_FirstSetValueToNlmOfArticleSchemaTypePropertyBeforeGet_ShouldSetTheValueOfArticleSchemaTypePropertyToNlm()
        {
            this.config.ArticleSchemaType = SchemaType.Nlm;
            Assert.AreEqual(SchemaType.Nlm, this.config.ArticleSchemaType, "ArticleSchemaType should be Nlm.");
        }

        [Test]
        public void Config_SetValueToSystemOfArticleSchemaTypePropertyAfterGetInNewInstance_ShouldNotChangeTheDefaultValue()
        {
            var dafaultValue = this.config.ArticleSchemaType;
            this.config.ArticleSchemaType = SchemaType.System;
            Assert.AreEqual(dafaultValue, this.config.ArticleSchemaType, "ArticleSchemaType should be equal to the default value.");
        }

        [Test]
        public void Config_SetValueToNlmOfArticleSchemaTypePropertyAfterGetInNewInstance_ShouldNotChangeTheDefaultValue()
        {
            var dafaultValue = this.config.ArticleSchemaType;
            this.config.ArticleSchemaType = SchemaType.Nlm;
            Assert.AreEqual(dafaultValue, this.config.ArticleSchemaType, "ArticleSchemaType should be equal to the default value.");
        }

        [TestCase(SchemaType.Nlm)]
        [TestCase(SchemaType.System)]
        public void Config_SetValueToSystemOfArticleSchemaTypePropertyAfterInitialSet_ShouldNotChangeTheInitialValue(SchemaType initialValue)
        {
            this.config.ArticleSchemaType = initialValue;
            this.config.ArticleSchemaType = SchemaType.System;
            Assert.AreEqual(initialValue, this.config.ArticleSchemaType, "ArticleSchemaType should be equal to the initial value.");
        }

        [TestCase(SchemaType.Nlm)]
        [TestCase(SchemaType.System)]
        public void Config_SetValueToNlmOfArticleSchemaTypePropertyAfterInitialSet_ShouldNotChangeTheInitialValue(SchemaType initialValue)
        {
            this.config.ArticleSchemaType = initialValue;
            this.config.ArticleSchemaType = SchemaType.Nlm;
            Assert.AreEqual(initialValue, this.config.ArticleSchemaType, "ArticleSchemaType should be equal to the initial value.");
        }

        [TestCase(SchemaType.Nlm)]
        [TestCase(SchemaType.System)]
        public void Config_SetValueToSystemOfArticleSchemaTypePropertyAfterGetAfterInitialSet_ShouldNotChangeTheInitialValue(SchemaType initialValue)
        {
            this.config.ArticleSchemaType = initialValue;

            var lastValue = this.config.ArticleSchemaType;
            Assert.AreEqual(initialValue, lastValue, "First set of values does not work correctly.");

            this.config.ArticleSchemaType = SchemaType.System;
            Assert.AreEqual(initialValue, this.config.ArticleSchemaType, "ArticleSchemaType should be equal to the initial value.");
        }

        [TestCase(SchemaType.Nlm)]
        [TestCase(SchemaType.System)]
        public void Config_SetValueToNlmOfArticleSchemaTypePropertyAfterGetAfterInitialSet_ShouldNotChangeTheInitialValue(SchemaType initialValue)
        {
            this.config.ArticleSchemaType = initialValue;

            var lastValue = this.config.ArticleSchemaType;
            Assert.AreEqual(initialValue, lastValue, "First set of values does not work correctly.");

            this.config.ArticleSchemaType = SchemaType.Nlm;
            Assert.AreEqual(initialValue, this.config.ArticleSchemaType, "ArticleSchemaType should be equal to the initial value.");
        }

        [Test]
        public void Config_TagWholeDocumentPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.config.TagWholeDocument, "Default value of the TagWholeDocument Property should be false.");
        }

        [Test]
        public void Config_FirstSetValueToFalseOfTagWholeDocumentPropertyBeforeGet_ShouldSetTheValueOfArticleSchemaTypePropertyToFalse()
        {
            this.config.TagWholeDocument = false;
            Assert.IsFalse(this.config.TagWholeDocument, "TagWholeDocument should be false.");
        }

        [Test]
        public void Config_FirstSetValueToTrueOfTagWholeDocumentPropertyBeforeGet_ShouldSetTheValueOfArticleSchemaTypePropertyToTrue()
        {
            this.config.TagWholeDocument = true;
            Assert.IsTrue(this.config.TagWholeDocument, "TagWholeDocument should be true.");
        }

        [Test]
        public void Config_SetValueToFalseOfTagWholeDocumentPropertyAfterGetInNewInstance_ShouldNotChangeTheDefaultValue()
        {
            bool dafaultValue = this.config.TagWholeDocument;
            this.config.TagWholeDocument = false;
            Assert.AreEqual(dafaultValue, this.config.TagWholeDocument, "TagWholeDocument should be equal to the default value.");
        }

        [Test]
        public void Config_SetValueToTrueOfTagWholeDocumentPropertyAfterGetInNewInstance_ShouldNotChangeTheDefaultValue()
        {
            bool dafaultValue = this.config.TagWholeDocument;
            this.config.TagWholeDocument = true;
            Assert.AreEqual(dafaultValue, this.config.TagWholeDocument, "TagWholeDocument should be equal to the default value.");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Config_SetValueToFalseOfTagWholeDocumentPropertyAfterInitialSet_ShouldNotChangeTheInitialValue(bool initialValue)
        {
            this.config.TagWholeDocument = initialValue;
            this.config.TagWholeDocument = false;
            Assert.AreEqual(initialValue, this.config.TagWholeDocument, "TagWholeDocument should be equal to the initial value.");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Config_SetValueToTrueOfTagWholeDocumentPropertyAfterInitialSet_ShouldNotChangeTheInitialValue(bool initialValue)
        {
            this.config.TagWholeDocument = initialValue;
            this.config.TagWholeDocument = true;
            Assert.AreEqual(initialValue, this.config.TagWholeDocument, "TagWholeDocument should be equal to the initial value.");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Config_SetValueToFalseOfTagWholeDocumentPropertyAfterGetAfterInitialSet_ShouldNotChangeTheInitialValue(bool initialValue)
        {
            this.config.TagWholeDocument = initialValue;

            bool lastValue = this.config.TagWholeDocument;
            Assert.AreEqual(initialValue, lastValue, "First set of values does not work correctly.");

            this.config.TagWholeDocument = false;
            Assert.AreEqual(initialValue, this.config.TagWholeDocument, "TagWholeDocument should be equal to the initial value.");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Config_SetValueToTrueOfTagWholeDocumentPropertyAfterGetAfterInitialSet_ShouldNotChangeTheInitialValue(bool initialValue)
        {
            this.config.TagWholeDocument = initialValue;

            bool lastValue = this.config.TagWholeDocument;
            Assert.AreEqual(initialValue, lastValue, "First set of values does not work correctly.");

            this.config.TagWholeDocument = true;
            Assert.AreEqual(initialValue, this.config.TagWholeDocument, "TagWholeDocument should be equal to the initial value.");
        }
    }
}