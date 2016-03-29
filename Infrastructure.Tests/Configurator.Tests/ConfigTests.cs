namespace ProcessingTools.Configurator.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ConfigTests
    {
        private const string SampleFilePath = @"C:\temp\DataFiles\sample.xsl";
        private Config config;

        [SetUp]
        public void Init()
        {
            this.config = new Config();
        }

        [Test]
        public void Config_ValidChangesOfBlackListCleanXslPathProperty_ShouldBePersistent()
        {
            this.config.BlackListCleanXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.BlackListCleanXslPath,
                "1. BlackListCleanXslPath should match SampleFilePath.");

            this.config.BlackListCleanXslPath = null;
            Assert.IsNull(
                this.config.BlackListCleanXslPath,
                "2. BlackListCleanXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfBlackListXmlFilePathProperty_ShouldBePersistent()
        {
            this.config.BlackListXmlFilePath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.BlackListXmlFilePath,
                "1. BlackListXmlFilePath should match SampleFilePath.");

            this.config.BlackListXmlFilePath = null;
            Assert.IsNull(
                this.config.BlackListXmlFilePath,
                "2. BlackListXmlFilePath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfCodesRemoveNonCodeNodesProperty_ShouldBePersistent()
        {
            this.config.CodesRemoveNonCodeNodes = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.CodesRemoveNonCodeNodes,
                "1. CodesRemoveNonCodeNodes should match SampleFilePath.");

            this.config.CodesRemoveNonCodeNodes = null;
            Assert.IsNull(
                this.config.CodesRemoveNonCodeNodes,
                "2. CodesRemoveNonCodeNodes should be null.");
        }

        [Test]
        public void Config_ValidChangesOfFloraDistrinctTaxaXslPathProperty_ShouldBePersistent()
        {
            this.config.FloraDistrinctTaxaXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.FloraDistrinctTaxaXslPath,
                "1. FloraDistrinctTaxaXslPath should match SampleFilePath.");

            this.config.FloraDistrinctTaxaXslPath = null;
            Assert.IsNull(
                this.config.FloraDistrinctTaxaXslPath,
                "2. FloraDistrinctTaxaXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfFloraExtractedTaxaListPathProperty_ShouldBePersistent()
        {
            this.config.FloraExtractedTaxaListPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.FloraExtractedTaxaListPath,
                "1. FloraExtractedTaxaListPath should match SampleFilePath.");

            this.config.FloraExtractedTaxaListPath = null;
            Assert.IsNull(
                this.config.FloraExtractedTaxaListPath,
                "2. FloraExtractedTaxaListPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfFloraExtractTaxaPartsOutputPathProperty_ShouldBePersistent()
        {
            this.config.FloraExtractTaxaPartsOutputPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.FloraExtractTaxaPartsOutputPath,
                "1. FloraExtractTaxaPartsOutputPath should match SampleFilePath.");

            this.config.FloraExtractTaxaPartsOutputPath = null;
            Assert.IsNull(
                this.config.FloraExtractTaxaPartsOutputPath,
                "2. FloraExtractTaxaPartsOutputPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfFloraExtractTaxaPartsXslPathProperty_ShouldBePersistent()
        {
            this.config.FloraExtractTaxaPartsXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.FloraExtractTaxaPartsXslPath,
                "1. FloraExtractTaxaPartsXslPath should match SampleFilePath.");

            this.config.FloraExtractTaxaPartsXslPath = null;
            Assert.IsNull(
                this.config.FloraExtractTaxaPartsXslPath,
                "2. FloraExtractTaxaPartsXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfFloraExtractTaxaXslPathProperty_ShouldBePersistent()
        {
            this.config.FloraExtractTaxaXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.FloraExtractTaxaXslPath,
                "1. FloraExtractTaxaXslPath should match SampleFilePath.");

            this.config.FloraExtractTaxaXslPath = null;
            Assert.IsNull(
                this.config.FloraExtractTaxaXslPath,
                "2. FloraExtractTaxaXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfFloraGenerateTemplatesXslPathProperty_ShouldBePersistent()
        {
            this.config.FloraGenerateTemplatesXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.FloraGenerateTemplatesXslPath,
                "1. FloraGenerateTemplatesXslPath should match SampleFilePath.");

            this.config.FloraGenerateTemplatesXslPath = null;
            Assert.IsNull(
                this.config.FloraGenerateTemplatesXslPath,
                "2. FloraGenerateTemplatesXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfFloraTemplatesOutputXmlPathProperty_ShouldBePersistent()
        {
            this.config.FloraTemplatesOutputXmlPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.FloraTemplatesOutputXmlPath,
                "1. FloraTemplatesOutputXmlPath should match SampleFilePath.");

            this.config.FloraTemplatesOutputXmlPath = null;
            Assert.IsNull(
                this.config.FloraTemplatesOutputXmlPath,
                "2. FloraTemplatesOutputXmlPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfFormatXslNlmToSystemProperty_ShouldBePersistent()
        {
            this.config.FormatNlmToSystemXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.FormatNlmToSystemXslPath,
                "1. FormatXslNlmToSystem should match SampleFilePath.");

            this.config.FormatNlmToSystemXslPath = null;
            Assert.IsNull(
                this.config.FormatNlmToSystemXslPath,
                "2. FormatXslNlmToSystem should be null.");
        }

        [Test]
        public void Config_ValidChangesOfMainDictionaryDataSourceStringProperty_ShouldBePersistent()
        {
            this.config.MainDictionaryDataSourceString = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.MainDictionaryDataSourceString,
                "1. MainDictionaryDataSourceString should match SampleFilePath.");

            this.config.MainDictionaryDataSourceString = null;
            Assert.IsNull(
                this.config.MainDictionaryDataSourceString,
                "2. MainDictionaryDataSourceString should be null.");
        }

        [Test]
        public void Config_ValidChangesOfRankListCleanXslPathProperty_ShouldBePersistent()
        {
            this.config.RankListCleanXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.RankListCleanXslPath,
                "1. RankListCleanXslPath should match SampleFilePath.");

            this.config.RankListCleanXslPath = null;
            Assert.IsNull(
                this.config.RankListCleanXslPath,
                "2. RankListCleanXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfRankListXmlFilePathProperty_ShouldBePersistent()
        {
            this.config.RankListXmlFilePath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.RankListXmlFilePath,
                "1. RankListXmlFilePath should match SampleFilePath.");

            this.config.RankListXmlFilePath = null;
            Assert.IsNull(
                this.config.RankListXmlFilePath,
                "2. RankListXmlFilePath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfReferencesGetReferencesXmlPathProperty_ShouldBePersistent()
        {
            this.config.ReferencesGetReferencesXmlPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.ReferencesGetReferencesXmlPath,
                "1. ReferencesGetReferencesXmlPath should match SampleFilePath.");

            this.config.ReferencesGetReferencesXmlPath = null;
            Assert.IsNull(
                this.config.ReferencesGetReferencesXmlPath,
                "2. ReferencesGetReferencesXmlPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfReferencesGetReferencesXslPathProperty_ShouldBePersistent()
        {
            this.config.ReferencesGetReferencesXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.ReferencesGetReferencesXslPath,
                "1. ReferencesGetReferencesXslPath should match SampleFilePath.");

            this.config.ReferencesGetReferencesXslPath = null;
            Assert.IsNull(
                this.config.ReferencesGetReferencesXslPath,
                "2. ReferencesGetReferencesXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfReferencesTagTemplateXmlPathProperty_ShouldBePersistent()
        {
            this.config.ReferencesTagTemplateXmlPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.ReferencesTagTemplateXmlPath,
                "1. ReferencesTagTemplateXmlPath should match SampleFilePath.");

            this.config.ReferencesTagTemplateXmlPath = null;
            Assert.IsNull(
                this.config.ReferencesTagTemplateXmlPath,
                "2. ReferencesTagTemplateXmlPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfReferencesTagTemplateXslPathProperty_ShouldBePersistent()
        {
            this.config.ReferencesTagTemplateXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.ReferencesTagTemplateXslPath,
                "1. ReferencesTagTemplateXslPath should match SampleFilePath.");

            this.config.ReferencesTagTemplateXslPath = null;
            Assert.IsNull(
                this.config.ReferencesTagTemplateXslPath,
                "2. ReferencesTagTemplateXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfTempDirectoryPathProperty_ShouldBePersistent()
        {
            this.config.TempDirectoryPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.TempDirectoryPath,
                "1. TempDirectoryPath should match SampleFilePath.");

            this.config.TempDirectoryPath = null;
            Assert.IsNull(
                this.config.TempDirectoryPath,
                "2. TempDirectoryPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfWhiteListCleanXslPathProperty_ShouldBePersistent()
        {
            this.config.WhiteListCleanXslPath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.WhiteListCleanXslPath,
                "1. WhiteListCleanXslPath should match SampleFilePath.");

            this.config.WhiteListCleanXslPath = null;
            Assert.IsNull(
                this.config.WhiteListCleanXslPath,
                "2. WhiteListCleanXslPath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfWhiteListXmlFilePathProperty_ShouldBePersistent()
        {
            this.config.WhiteListXmlFilePath = SampleFilePath;
            Assert.AreEqual(
                SampleFilePath,
                this.config.WhiteListXmlFilePath,
                "1. WhiteListXmlFilePath should match SampleFilePath.");

            this.config.WhiteListXmlFilePath = null;
            Assert.IsNull(
                this.config.WhiteListXmlFilePath,
                "2. WhiteListXmlFilePath should be null.");
        }

        [Test]
        public void Config_ValidChangesOfEnvoResponseOutputXmlFileNameProperty_ShouldBePersistent()
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
        public void Config_ValidChangesOfGnrOutputFileNameProperty_ShouldBePersistent()
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