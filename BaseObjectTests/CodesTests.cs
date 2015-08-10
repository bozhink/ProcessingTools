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

        [TestMethod]
        [Timeout(10000)]
        public void Test_TagKnownSpecimenCodes()
        {
            Codes codes = new Codes(config, TestResourceStrings.String10);

            writer.WriteNode(codes.Xml.ToXmlReader(), true);

            List<SpecimenCode> prefixNumeric = codes.GetPrefixNumericCodes();
            foreach (SpecimenCode code in prefixNumeric)
            {
                Console.WriteLine("{0} -> {1}", code.Prefix, code.Code);
            }

            codes.TagKnownSpecimenCodes(xpathProvider);

            writer.WriteNode(codes.Xml.ToXmlReader(), true);
        }
    }
}
