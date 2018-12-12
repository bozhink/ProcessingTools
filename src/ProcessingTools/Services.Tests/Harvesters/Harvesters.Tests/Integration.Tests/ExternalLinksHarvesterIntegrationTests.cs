﻿namespace ProcessingTools.Harvesters.Tests.Integration.Tests
{
    using System.IO;
    using System.Linq;
    using System.Xml;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ProcessingTools.Common.Code.Serialization;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Harvesters;
    using ProcessingTools.Harvesters.Contracts.ExternalLinks;
    using ProcessingTools.Harvesters.ExternalLinks;
    using ProcessingTools.Harvesters.Models.Contracts.ExternalLinks;
    using ProcessingTools.Services.IO;
    using ProcessingTools.Services.Xml;

    [TestClass]
    public class ExternalLinksHarvesterIntegrationTests
    {
        /// <summary>
        /// Gets or sets the <see cref="TestContext"/>.
        /// </summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        [Timeout(5000)]
        public void ExternalLinksHarvester_WithSampleXmlFile_ShouldExtractCorrectlyExternalLinks()
        {
            // Arrange
            const int ExpectedNumberOfExternalLinks = 10;
            const int ExpectedNumberOfExternalLinksOfTypeDoi = 9;

            var xmlFileName = Path.Combine("Samples", "article -with-external-links.xml");
            var document = new XmlDocument
            {
                PreserveWhitespace = true
            };

            document.Load(xmlFileName);

            var contextWrapper = new XmlContextWrapper();
            var harvesterCore = new EnumerableXmlHarvesterCore<IExternalLinkModel>(contextWrapper);

            var deserializer = new XmlDeserializer();
            var serializer = new XmlTransformDeserializer(deserializer);

            var xslCache = new XslTransformCacheFromFile();
            var transformer = new XslTransformerFromFile(
                AppSettings.ExternalLinksXslFileName,
                xslCache,
                new XmlReadService());
            var transformerFactoryMock = new Mock<IExternalLinksTransformerFactory>();
            transformerFactoryMock
                .Setup(f => f.GetExternalLinksTransformer())
                .Returns(transformer);

            var harvester = new ExternalLinksHarvester(harvesterCore, serializer, transformerFactoryMock.Object);

            // Act
            var externalLinks = harvester.HarvestAsync(document.DocumentElement).Result?.ToList();

            // Assert
            Assert.IsNotNull(externalLinks);
            externalLinks?.ForEach(i => this.TestContext.WriteLine("{0} | {1} | {2}", i.BaseAddress, i.Uri, i.Value));

            Assert.AreEqual(ExpectedNumberOfExternalLinks, externalLinks?.Count);

            var doiExternalLinks = externalLinks?.Where(i => i.BaseAddress.Contains("doi.org")).ToList();
            Assert.AreEqual(ExpectedNumberOfExternalLinksOfTypeDoi, doiExternalLinks?.Count);
        }
    }
}
