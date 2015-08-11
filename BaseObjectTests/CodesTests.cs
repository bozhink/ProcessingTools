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
        [Timeout(10000)]
        public void Test_TagCodes_String7()
        {
            Codes codes = new Codes(config, TestResourceStrings.String7);
            DataProvider dataProvider = new DataProvider(config, codes.Xml);

            writer.WriteNode(codes.Xml.ToXmlReader(), true);

            codes.TagKnownSpecimenCodes(xpathProvider);
            codes.TagInstitutions(xpathProvider, dataProvider);
            codes.TagInstitutionalCodes(xpathProvider, dataProvider);
            codes.TagSpecimenCodes(xpathProvider);

            writer.WriteNode(codes.Xml.ToXmlReader(), true);
        }

    }
}
