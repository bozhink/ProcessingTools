namespace ProcessingTools.BaseLibrary.Tests
{
    using Configurator;
    using Contracts;
    using Contracts.Log;
    using Loggers;
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
            logger = new TextWriterLogger();
        }
    }
}
