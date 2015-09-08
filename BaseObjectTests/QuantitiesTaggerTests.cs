using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcessingTools;
using ProcessingTools.BaseLibrary;

namespace BaseObjectTests
{
    [TestClass]
    public class QuantitiesTaggerTests
    {
        private Config config;
        private XmlTextWriter writer;
        private XPathProvider xpathProvider;

        [TestInitialize]
        public void Test_Initialize()
        {
            this.config = ConfigBuilder.CreateConfig(@"C:\bin\config.json");
            this.xpathProvider = new XPathProvider(this.config);
            this.writer = new XmlTextWriter(Console.Out);
            this.writer.Formatting = Formatting.Indented;
        }

        [TestCleanup]
        public void Test_Cleanup()
        {
            writer.Close();
        }

        [TestMethod]
        public void QuantitiesTagger_CreateNewInstance_WithValidConfigAndXml()
        {
            string xml = "<x></x>";
            QuantitiesTagger quantitiesTagger = new QuantitiesTagger(config, xml);
            Assert.AreEqual(config.tempDirectoryPath, quantitiesTagger.Config.tempDirectoryPath, "Temp directory path does not match.");
            Assert.AreEqual(xml, quantitiesTagger.Xml, "Xml content is not valid.");
            Assert.AreEqual(xml, quantitiesTagger.XmlDocument.OuterXml, "Xml content in XmlDocument is not as expected.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void QuantitiesTagger_CreateNewInstance_WithValidConfigAndNullXml()
        {
            QuantitiesTagger quantitiesTagger = new QuantitiesTagger(config, null);
        }

        //[TestMethod]
        //public void QuantitiesTagger_CreateNewInstance_WithValidIBaseObject()
        //{
        //    string xml = "<x></x>";
        //    QuantitiesTagger quantitiesTagger1 = new QuantitiesTagger(config, xml);
        //    QuantitiesTagger quantitiesTagger2 = new QuantitiesTagger(quantitiesTagger1);
        //    Assert.AreEqual(quantitiesTagger1.Config, quantitiesTagger2.Config, "Configs are different.");
        //    Assert.AreEqual(quantitiesTagger1.Xml, quantitiesTagger2.Xml, "Xml content differs.");
        //}

        [TestMethod]
        public void QunatitiesTagger_XPathExecutionTests()
        {
            XmlDocument xml = new XmlDocument(Config.TaxPubNamespceManager().NameTable);
            xml.PreserveWhitespace = true;
            xml.Load(@"C:\Users\bozhin\SkyDrive\Work\9949-abbrev.xml");

            QuantitiesTagger qt = new QuantitiesTagger(config, xml.OuterXml);
            Assert.AreEqual(
                xml.SelectNodes("//p").Count,
                qt.XmlDocument.SelectNodes("//p").Count,
                "Number of paragraphs is different.");
        }

        [TestMethod]
        public void QuantitiesTagger_TagTest()
        {
            XmlDocument xml = new XmlDocument(Config.TaxPubNamespceManager().NameTable);
            xml.PreserveWhitespace = true;
            xml.Load(@"C:\Users\bozhin\SkyDrive\Work\9949-abbrev.xml");

            QuantitiesTagger qt = new QuantitiesTagger(config, xml.OuterXml);
            qt.TagQuantities(xpathProvider);
            qt.TagDeviation(xpathProvider);
            qt.TagAltitude(xpathProvider);

            Console.WriteLine(qt.Xml);
        }
    }
}
