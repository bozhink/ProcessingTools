namespace BaseObjectTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.BaseLibrary.Measurements;

    [TestClass]
    public class SimpleTests
    {
        const string pathToConfigFile = @"C:\bin\config.json";

        [TestMethod]
        [Ignore]
        public void  CreateNewInstanceOfDataProviderWithValidInputData()
        {
            string xmlText = "<article></article>";
            Config config = ConfigBuilder.CreateConfig(pathToConfigFile);

            DataProvider dataProvider = new DataProvider(config, xmlText);
            Assert.AreEqual(dataProvider.Xml, xmlText);
        }

        [TestMethod]
        [Ignore]
        public void CreateNewInstanceOfDataProviderWithValidInputDataInUsingBlock()
        {
            string xmlText = "<article></article>";
            string resultXmlText;
            Config config = ConfigBuilder.CreateConfig(pathToConfigFile);

            using (DataProvider dataProvider = new DataProvider(config, xmlText))
            {
                resultXmlText = dataProvider.Xml;
            }

            Assert.AreEqual(resultXmlText, xmlText);
        }
    }
}
