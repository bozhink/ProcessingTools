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

        [TestMethod]
        public void TestMethod1()
        {
            Config config = ConfigBuilder.CreateConfig(@"C:\bin\config.json");
            XPathProvider xpathProvider = new XPathProvider(config);
            Codes codes = new Codes(config, TestResourceStrings.String10);

            Console.WriteLine(codes.Xml.ApplyXslTransform(config.copyXslFilePath));

            List<SpecimenCode> prefixNumeric = codes.GetPrefixNumericCodes();
            foreach (SpecimenCode code in prefixNumeric)
            {
                Console.WriteLine("{0} -> {1}", code.Prefix, code.Code);
            }

            codes.TagKnownSpecimenCodes(xpathProvider);

            Console.WriteLine(codes.Xml.ApplyXslTransform(config.copyXslFilePath));
        }
    }
}
