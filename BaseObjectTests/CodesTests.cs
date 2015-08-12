using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcessingTools;
using ProcessingTools.Base;
using System.Xml.XPath;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace BaseObjectTests
{
    [TestClass]
    public class CodesTests
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

        public void TagKnownSpecimenCodes_Tests(string xmlString)
        {
            Codes codes = new Codes(config, xmlString);

            writer.WriteNode(codes.Xml.ToXmlReader(), true);

            List<SpecimenCode> prefixNumeric = codes.GetPrefixNumericCodes();
            foreach (SpecimenCode code in prefixNumeric)
            {
                Console.WriteLine("{0} -> {1}", code.Prefix, code.Code);
            }

            codes.TagKnownSpecimenCodes(xpathProvider);

            writer.WriteNode(codes.Xml.ToXmlReader(), true);
        }

        [TestMethod]
        [Timeout(10000)]
        public void Test_TagKnownSpecimenCodes_String7()
        {
            TagKnownSpecimenCodes_Tests(TestResourceStrings.String7);
        }

        [TestMethod]
        [Timeout(10000)]
        public void Test_TagKnownSpecimenCodes_String10()
        {
            TagKnownSpecimenCodes_Tests(TestResourceStrings.String10);
        }

        [TestMethod]
        [Timeout(5000)]
        public void Test_ListTablesFromDatabaseProvider_String7()
        {
            using (DataProvider dp = new DataProvider(config, TestResourceStrings.String7))
            {
                foreach(string name in dp.ListTables())
                {
                    Console.WriteLine(name);
                }
            }
        }

        [TestMethod]
        [Timeout(10000)]
        public void Test_TagCodes_String7()
        {
            Codes codes = new Codes(config, TestResourceStrings.String7);
            using (DataProvider dataProvider = new DataProvider(config, codes.Xml))
            {

                {
                    SpecimenCountTagger sp = new SpecimenCountTagger(config, codes.Xml);
                    sp.TagSpecimenCount(xpathProvider);
                    codes.Xml = sp.Xml;
                }

                writer.WriteNode(codes.Xml.ToXmlReader(), true);

                codes.TagKnownSpecimenCodes(xpathProvider);
                codes.TagInstitutions(xpathProvider, dataProvider);
                codes.TagInstitutionalCodes(xpathProvider, dataProvider);
                codes.TagSpecimenCodes(xpathProvider);

                writer.WriteNode(codes.Xml.ToXmlReader(), true);
            }
        }

        [TestMethod]
        [Timeout(10000)]
        public void Test_TagEnvo_10154()
        {
            config.EnvoResponseOutputXmlFileName = @"C:\temp\envo-out.xml";

            Envo envo = new Envo(config, TestResourceStrings.ZK10154);

            envo.Tag();

            writer.WriteNode(envo.Xml.ToXmlReader(), true);
        }

        [TestMethod]
        [Timeout(10000)]
        public void Test_TagCodes_10154()
        {
            config.EnvoResponseOutputXmlFileName = @"C:\temp\envo-out.xml";

            Codes codes = new Codes(config, TestResourceStrings.ZK10154);
            DataProvider dataProvider = new DataProvider(config, codes.Xml);

            codes.TagKnownSpecimenCodes(xpathProvider);

            {
                Envo envo = new Envo(config, codes.Xml);
                envo.Tag();
                codes.Xml = envo.Xml;
            }

            {
                AbbreviationsTagger abbr = new AbbreviationsTagger(config, codes.Xml);
                abbr.TagAbbreviationsInText();
                codes.Xml = abbr.Xml;
            }

            {
                DatesTagger dates = new DatesTagger(config, codes.Xml);
                dates.TagDates(xpathProvider);
                codes.Xml = dates.Xml;
            }

            {
                QuantitiesTagger quant = new QuantitiesTagger(config, codes.Xml);
                quant.TagQuantities(xpathProvider);
                quant.TagDirections(xpathProvider);
                quant.TagAltitude(xpathProvider);
                codes.Xml = quant.Xml;
            }

            {
                SpecimenCountTagger sc = new SpecimenCountTagger(config, codes.Xml);
                sc.TagSpecimenCount(xpathProvider);
                codes.Xml = sc.Xml;
            }

            {
                GeoNamesTagger geo = new GeoNamesTagger(config, codes.Xml);
                geo.TagGeonames(xpathProvider, dataProvider);
                codes.Xml = geo.Xml;
            }

            
            codes.TagInstitutions(xpathProvider, dataProvider);
            codes.TagInstitutionalCodes(xpathProvider, dataProvider);
            codes.TagSpecimenCodes(xpathProvider);

            writer.WriteNode(codes.Xml.ToXmlReader(), true);
        }

    }
}
