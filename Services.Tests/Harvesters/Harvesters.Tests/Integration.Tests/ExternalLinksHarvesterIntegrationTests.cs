namespace ProcessingTools.Harvesters.Tests.Integration.Tests
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Xml;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Harvesters.Contracts.Transformers;
    using ProcessingTools.Harvesters.Harvesters.ExternalLinks;
    using ProcessingTools.Harvesters.Transformers;
    using ProcessingTools.Serialization.Serializers;
    using ProcessingTools.Xml.Cache;
    using ProcessingTools.Xml.Providers;
    using ProcessingTools.Xml.Serialization;

    [TestClass]
    public class ExternalLinksHarvesterIntegrationTests
    {
        [TestMethod]
        [Timeout(5000)]
        public void ExternalLinksHarvester_WithSampleXmlFile_ShouldExtractCorrectlyExternalLinks()
        {
            // Arrange
            const int ExpectedNumberOfExternalLinks = 10;
            const int ExpectedNumberOfExternalLinksOfTypeDoi = 9;

            var xmlFileName = $"{ConfigurationManager.AppSettings["SampleFiles"]}/article-with-external-links.xml";
            var document = new XmlDocument
            {
                PreserveWhitespace = true
            };

            document.Load(xmlFileName);

            var contextWrapperProvider = new XmlContextWrapperProvider();

            var deserializer = new XmlDeserializer();
            var xslCache = new XslTransformCache();
            var transformProvider = new GetExternalLinksXslTransformProvider(xslCache);
            var transformer = new XmlTransformDeserializer<IGetExternalLinksTransformer>(new GetExternalLinksTransformer(transformProvider), deserializer);

            var harvester = new ExternalLinksHarvester(contextWrapperProvider, transformer);

            // Act
            var externalLinks = harvester.Harvest(document.DocumentElement).Result?.ToList();

            // Assert
            Assert.IsNotNull(externalLinks);
            externalLinks.ForEach(i => Console.WriteLine("{0} | {1} | {2}", i.BaseAddress, i.Uri, i.Value));

            Assert.AreEqual(ExpectedNumberOfExternalLinks, externalLinks.Count);

            var doiExternalLinks = externalLinks.Where(i => i.BaseAddress.Contains("doi.org")).ToList();
            Assert.AreEqual(ExpectedNumberOfExternalLinksOfTypeDoi, doiExternalLinks.Count);
        }
    }
}
