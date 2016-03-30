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