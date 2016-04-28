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
    }
}