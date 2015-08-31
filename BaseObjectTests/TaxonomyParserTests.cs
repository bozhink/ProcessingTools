namespace ProcessingTools.Base.Taxonomy
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TaxonomyParserTests
    {
        private Config config;

        [TestInitialize]
        public void TestInit()
        {
            config = ConfigBuilder.CreateConfig(@"C:\bin\config.json");
        }
        
        [TestMethod]
        public void SuffixHigherTaxaParser_CreateNewInstance_SchouldSucceed()
        {
            string xml = "<article></article>";
            var parser = new SuffixHigherTaxaParser(this.config, xml);

            Assert.IsTrue(parser is SuffixHigherTaxaParser, "Parser is not a SuffixHigherTaxaParser object.");
            Assert.IsTrue(parser is HigherTaxaParser, "Parser is not a HigherTaxaParser object.");
            Assert.IsTrue(parser is Base, "Parser is not a Base object.");
            Assert.IsTrue(parser is IBase, "Parser is not a Base object.");
            Assert.IsTrue(parser is IParser, "Parser is not a Base object.");

            Assert.AreEqual(xml, parser.Xml, "Parser xml is not valid before Parse().");
            parser.Parse();
            Assert.AreEqual(xml, parser.Xml, "Parser xml is not valid afterr Parse().");
        }
    }
}
