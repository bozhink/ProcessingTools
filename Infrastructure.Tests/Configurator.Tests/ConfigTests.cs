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
    }
}