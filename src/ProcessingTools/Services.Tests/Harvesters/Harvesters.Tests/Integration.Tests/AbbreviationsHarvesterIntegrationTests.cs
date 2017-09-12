namespace ProcessingTools.Harvesters.Tests.Integration.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Harvesters.Contracts.Factories;
    using ProcessingTools.Harvesters.Harvesters.Abbreviations;
    using ProcessingTools.Serialization.Serializers;
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

            var harvester = new AbbreviationsHarvester(contextWrapper, serializer, transformersFactoryMock.Object);

            // Act
            var abbreviations = harvester.Harvest(document.DocumentElement).Result?.ToList();

            Assert.IsNotNull(abbreviations);
            abbreviations?.ForEach(i => Console.WriteLine("{0} | {1} | {2}", i.Value, i.ContentType, i.Definition));

            Assert.AreEqual(ExpectedNumberOfAbbreviations, abbreviations?.Count);
        }
    }
}
