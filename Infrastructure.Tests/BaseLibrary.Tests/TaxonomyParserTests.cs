namespace ProcessingTools.BaseLibrary.Tests
{
    using Loggers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Configurator;
    using ProcessingTools.Contracts;

    [TestClass]
    public class TaxonomyParserTests
    {
        private static Config config;
        private static ILogger logger;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            config = ConfigBuilder.Create(@"C:\bin\config.json");
            logger = new TextWriterLogger();
        }
    }
}