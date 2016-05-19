namespace ProcessingTools.Harvesters.Tests
{
    using System.Configuration;
    using System.Linq;
    using System.Xml;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExternalLinksHarvesterTests
    {
        [TestMethod]
        public void ExternalLinksHarvester_WithDefaultConstructor_ShouldReturnValidObject()
        {
            var harvester = new ExternalLinksHarvester();

            Assert.IsNotNull(harvester, "Harvester should not be null.");
        }

        [TestMethod]
        public void ExternalLinksHarvester_WithSampleXmlFile_ShouldExtractCorrectlyExternalLinks()
        {
            var xmlFileName = $"{ConfigurationManager.AppSettings["SampleFiles"]}/article-with-external-links.xml";
            XmlDocument document = new XmlDocument();
            document.Load(xmlFileName);

            var harvester = new ExternalLinksHarvester();

            Assert.IsNotNull(harvester, "Harvester should not be null.");

            var items = harvester.Harvest(document.DocumentElement).Result?.ToList();

            Assert.IsNotNull(items, "Items should not be null.");

            items.ForEach(i => System.Console.WriteLine("{0} | {1} | {2}", i.BaseAddress, i.Uri, i.Value));

            Assert.AreEqual(10, items.Count, "Number of external links should be 10.");

            var doiExternalLinks = items.Where(i => i.BaseAddress.Contains("dx.doi.org")).ToList();
            Assert.AreEqual(9, doiExternalLinks.Count, "Number of DOI external links should be 9.");
        }
    }
}