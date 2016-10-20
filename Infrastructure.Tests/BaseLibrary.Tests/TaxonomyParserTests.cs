namespace ProcessingTools.BaseLibrary.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Contracts;
    using ProcessingTools.Loggers;

    [TestClass]
    public class TaxonomyParserTests
    {
        private static ILogger logger;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            logger = new ConsoleLogger();
        }
    }
}
