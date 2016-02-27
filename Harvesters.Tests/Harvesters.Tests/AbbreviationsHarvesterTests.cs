namespace ProcessingTools.Harvesters.Tests
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Xml;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AbbreviationsHarvesterTests
    {
        [TestMethod]
        public void AbbreviationsHarvester_WithDefaultConstructor_ShouldReturnValidObject()
        {
            var harvester = new AbbreviationsHarvester();

            Assert.IsNotNull(harvester, "Harvester should not be null");
        }

        [TestMethod]
        public void AbbreviationsHarvester_HarvestSampleDocument_ShouldSucceed()
        {
            var xmlFileName = $"{ConfigurationManager.AppSettings["SampleFiles"]}/article-with-abbrev.xml";
            XmlDocument document = new XmlDocument();
            document.Load(xmlFileName);

            var harvester = new AbbreviationsHarvester();

            Assert.IsNotNull(harvester, "Harvester should not be null");

            var items = harvester.Harvest(document.DocumentElement).Result?.ToList();

            Assert.IsNotNull(items, "Items should not be null.");

            items.ForEach(i => Console.WriteLine("{0} | {1} | {2}", i.Value, i.ContentType, i.Definition));

            Assert.AreEqual(22, items.Count, "Number of abbreviations should be 22.");
        }
    }
}