namespace ProcessingTools.BaseLibrary.Tests
{
    using System;
    using System.Xml;

    using Configurator;
    using DocumentProvider;
    using Loggers;
    using Measurements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Contracts.Log;

    [TestClass]
    public class QuantitiesTaggerTests
    {
        private static Config config;
        private static XmlTextWriter writer;
        private static XPathProvider xpathProvider;
        private static ILogger logger;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            config = ConfigBuilder.CreateConfig(@"C:\bin\config.json");
            xpathProvider = new XPathProvider(config);
            writer = new XmlTextWriter(Console.Out);
            writer.Formatting = Formatting.Indented;
            logger = new TextWriterLogger();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            writer.Close();
        }       

        [TestMethod]
        public void QuantitiesTagger_CreateNewInstance_WithValidConfigAndXml()
        {
            string xml = "<x></x>";
            QuantitiesTagger quantitiesTagger = new QuantitiesTagger(config, xml, logger);
            Assert.AreEqual(config.TempDirectoryPath, quantitiesTagger.Config.TempDirectoryPath, "Temp directory path does not match.");
            Assert.AreEqual(xml, quantitiesTagger.Xml, "Xml content is not valid.");
            Assert.AreEqual(xml, quantitiesTagger.XmlDocument.OuterXml, "Xml content in XmlDocument is not as expected.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void QuantitiesTagger_CreateNewInstance_WithValidConfigAndNullXml()
        {
            QuantitiesTagger quantitiesTagger = new QuantitiesTagger(config, null, logger);
        }

        [TestMethod]
        public void QuantitiesTagger_CreateNewInstance_WithValidIBaseObject()
        {
            string xml = "<x></x>";
            QuantitiesTagger quantitiesTagger1 = new QuantitiesTagger(config, xml, logger);
            QuantitiesTagger quantitiesTagger2 = new QuantitiesTagger(quantitiesTagger1, logger);
            Assert.AreEqual(quantitiesTagger1.Config, quantitiesTagger2.Config, "Configs are different.");
            Assert.AreEqual(quantitiesTagger1.Xml, quantitiesTagger2.Xml, "Xml content differs.");
        }

        [TestMethod]
        [Ignore]
        public void QunatitiesTagger_XPathExecutionTests()
        {
            XmlDocument xml = new XmlDocument(TaxPubDocument.NamespceManager().NameTable);
            xml.PreserveWhitespace = true;
            xml.Load(@"C:\Users\bozhin\SkyDrive\Work\9949-abbrev.xml");

            QuantitiesTagger qt = new QuantitiesTagger(config, xml.OuterXml, logger);
            Assert.AreEqual(
                xml.SelectNodes("//p").Count,
                qt.XmlDocument.SelectNodes("//p").Count,
                "Number of paragraphs is different.");
        }

        [TestMethod]
        [Ignore]
        public void QuantitiesTagger_TagTest()
        {
            XmlDocument xml = new XmlDocument(TaxPubDocument.NamespceManager().NameTable);
            xml.PreserveWhitespace = true;
            xml.Load(@"C:\Users\bozhin\SkyDrive\Work\9949-abbrev.xml");

            QuantitiesTagger qt = new QuantitiesTagger(config, xml.OuterXml, logger);
            qt.TagQuantities(xpathProvider);
            qt.TagDeviation(xpathProvider);
            qt.TagAltitude(xpathProvider);

            Console.WriteLine(qt.Xml);
        }
    }
}
