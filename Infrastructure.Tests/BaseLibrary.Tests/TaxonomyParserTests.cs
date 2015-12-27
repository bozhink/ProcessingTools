namespace ProcessingTools.BaseLibrary.Tests
{
    using Configurator;
    using Loggers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Contracts.Log;

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