namespace ProcessingTools.BaseLibrary.Tests
{
    using Configurator;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        [TestMethod]
        [Ignore]
        public void TestAphiaService()
        {
            var server = new Globals.Services.AphiaNameService();
            var records = server.getAphiaRecords("Anodontiglanis", true, true, false, 0);

            System.Console.WriteLine(records?.Length);
            if (records != null)
            {
                foreach (Globals.Services.AphiaRecord record in records)
                {
                    System.Console.WriteLine(record?.rank);
                    System.Console.WriteLine(record?.scientificname);
                    System.Console.WriteLine(record?.valid_name);
                }
            }
        }
    }
}
