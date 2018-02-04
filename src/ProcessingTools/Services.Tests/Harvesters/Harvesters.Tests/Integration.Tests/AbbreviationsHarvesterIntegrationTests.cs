namespace ProcessingTools.Harvesters.Tests.Integration.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ProcessingTools.Common.Serialization;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Contracts.Harvesters.Abbreviations;
    using ProcessingTools.Models.Contracts.Harvesters.Abbreviations;
    using ProcessingTools.Harvesters;
    using ProcessingTools.Harvesters.Abbreviations;
    using ProcessingTools.Xml.Cache;
    using ProcessingTools.Xml.Serialization;
    using ProcessingTools.Xml.Transformers;
    using ProcessingTools.Xml.Wrappers;

    [TestClass]
    public class AbbreviationsHarvesterIntegrationTests
    {
        [TestMethod]
        [Timeout(5000)]
        public void AbbreviationsHarvester_HarvestSampleDocument_ShouldSucceed()
        {
            // Arrange
            const int ExpectedNumberOfAbbreviations = 22;

            var xmlFileName = Path.Combine(AppSettings.SampleFiles, "article-with-abbrev.xml");
            XmlDocument document = new XmlDocument
            {
                PreserveWhitespace = true
            };

            document.Load(xmlFileName);

            var contextWrapper = new XmlContextWrapper();
            var harvesterCore = new EnumerableXmlHarvesterCore<IAbbreviationModel>(contextWrapper);

            var deserializer = new XmlDeserializer();
            var serializer = new XmlTransformDeserializer(deserializer);

            var xqueryCache = new XQueryTransformCache();
            var transformer = new XQueryTransformer(
                AppSettings.AbbreviationsXQueryFileName,
                xqueryCache);
            var transformersFactoryMock = new Mock<IAbbreviationsTransformersFactory>();
            transformersFactoryMock
                .Setup(f => f.GetAbbreviationsTransformer())
                .Returns(transformer);

            var harvester = new AbbreviationsHarvester(harvesterCore, serializer, transformersFactoryMock.Object);

            // Act
            var abbreviations = harvester.HarvestAsync(document.DocumentElement).Result?.ToList();

            Assert.IsNotNull(abbreviations);
            abbreviations?.ForEach(i => Console.WriteLine("{0} | {1} | {2}", i.Value, i.ContentType, i.Definition));

            Assert.AreEqual(ExpectedNumberOfAbbreviations, abbreviations?.Count);
        }
    }
}
