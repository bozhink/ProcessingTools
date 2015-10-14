namespace BaseObjectTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools;
    using ProcessingTools.BaseLibrary;

    [TestClass]
    public class SimpleTests
    {
        private static Config config;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            config = ConfigBuilder.CreateConfig(@"C:\bin\config.json");
        }

        [TestMethod]
        [Ignore]
        public void CreateNewInstanceOfDataProviderWithValidInputData()
        {
            string xmlText = "<article></article>";

            DataProvider dataProvider = new DataProvider(config, xmlText);
            Assert.AreEqual(dataProvider.Xml, xmlText);
        }

        [TestMethod]
        [Ignore]
        public void CreateNewInstanceOfDataProviderWithValidInputDataInUsingBlock()
        {
            string xmlText = "<article></article>";
            string resultXmlText;

            DataProvider dataProvider = new DataProvider(config, xmlText);
            resultXmlText = dataProvider.Xml;

            Assert.AreEqual(resultXmlText, xmlText);
        }
    }
}
