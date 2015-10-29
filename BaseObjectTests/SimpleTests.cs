namespace ProcessingTools.BaseLibrary.Tests
{
    using Configurator;
    using Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Services.Aphia;

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

            var loggerMock = new Mock<ILogger>();
            DataProvider dataProvider = new DataProvider(config, xmlText, loggerMock.Object);
            Assert.AreEqual(dataProvider.Xml, xmlText);
        }

        [TestMethod]
        [Ignore]
        public void CreateNewInstanceOfDataProviderWithValidInputDataInUsingBlock()
        {
            string xmlText = "<article></article>";
            string resultXmlText;

            var loggerMock = new Mock<ILogger>();
            DataProvider dataProvider = new DataProvider(config, xmlText, loggerMock.Object);
            resultXmlText = dataProvider.Xml;

            Assert.AreEqual(resultXmlText, xmlText);
        }

        [TestMethod]
        [Ignore]
        public void TestAphiaService()
        {
            var server = new AphiaNameService();
            var records = server.getAphiaRecords("Anodontiglanis", true, true, false, 0);

            System.Console.WriteLine(records?.Length);
            if (records != null)
            {
                foreach (AphiaRecord record in records)
                {
                    System.Console.WriteLine(record?.rank);
                    System.Console.WriteLine(record?.scientificname);
                    System.Console.WriteLine(record?.valid_name);
                }
            }
        }
    }
}
