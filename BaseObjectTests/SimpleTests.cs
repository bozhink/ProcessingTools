namespace BaseObjectTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools;
    using ProcessingTools.BaseLibrary;

    [TestClass]
    public class SimpleTests
    {
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void TestMethod1()
        //{
        //    XPathProvider xpathProvider = new ProcessingTools.Base.XPathProvider(new Config());
        //}

        const string pathToConfigFile = @"C:\bin\config.json";

        [TestMethod]
        public void CreateNewInstanceOfQuantitiesTaggerWithValidInputData()
        {
            string xmlText = "<article></article>";

            Config config = ConfigBuilder.CreateConfig(pathToConfigFile);

            QuantitiesTagger quantitiesTagger = new QuantitiesTagger(config, xmlText);
            Assert.AreEqual(quantitiesTagger.Xml, xmlText);
        }

        [TestMethod]
        public void  CreateNewInstanceOfDataProviderWithValidInputData()
        {
            string xmlText = "<article></article>";
            Config config = ConfigBuilder.CreateConfig(pathToConfigFile);

            DataProvider dataProvider = new DataProvider(config, xmlText);
            Assert.AreEqual(dataProvider.Xml, xmlText);
        }

        [TestMethod]
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
