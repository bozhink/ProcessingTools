﻿namespace ProcessingTools.BaseLibrary.Tests
{
    using Configurator;
    using Globals;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Taxonomy;

    [TestClass]
    public class TaxonomyParserTests
    {
        private static Config config;
        private static ILogger logger;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            config = ConfigBuilder.CreateConfig(@"C:\bin\config.json");
            logger = new ConsoleLogger();
        }

        [TestMethod]
        [Ignore]
        public void SuffixHigherTaxaParser_CreateNewInstance_SchouldSucceed()
        {
            string xml = "<article></article>";
            var parser = new SuffixHigherTaxaParser(config, xml, logger);

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
