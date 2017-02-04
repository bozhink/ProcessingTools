namespace ProcessingTools.Harvesters.Tests.Integration.Tests
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Xml;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Harvesters.Contracts.Factories;
    using ProcessingTools.Harvesters.Harvesters.Abbreviations;
    using ProcessingTools.Serialization.Serializers;
    using ProcessingTools.Xml.Cache;
    using ProcessingTools.Xml.Wrappers;
    using ProcessingTools.Xml.Serialization;
    using ProcessingTools.Xml.Transformers;

    [TestClass]
    public class AbbreviationsHarvesterIntegrationTests
    {
        [TestMethod]
        [Timeout(5000)]
        public void AbbreviationsHarvester_HarvestSampleDocument_ShouldSucceed()
        {
            // Arrange
            const int ExpectedNumberOfAbbreviations = 22;

            var xmlFileName = $"{ConfigurationManager.AppSettings["SampleFiles"]}/article-with-abbrev.xml";
            XmlDocument document = new XmlDocument
            {
                PreserveWhitespace = true
            };

            document.Load(xmlFileName);

            var contextWrapperProvider = new XmlContextWrapperProvider();

            var deserializer = new XmlDeserializer();
            var serializer = new XmlTransformDeserializer(deserializer);

            var xqueryCache = new XQueryTransformCache();
            var transformer = new XQueryTransformer(
                ConfigurationManager.AppSettings[AppSettingsKeys.AbbreviationsXQueryFileName],
                xqueryCache);
            var transformersFactoryMock = new Mock<IAbbreviationsTransformersFactory>();
            transformersFactoryMock
                .Setup(f => f.GetAbbreviationsTransformer())
                .Returns(transformer);

            var harvester = new AbbreviationsHarvester(contextWrapperProvider, serializer, transformersFactoryMock.Object);

            // Act
            var abbreviations = harvester.Harvest(document.DocumentElement).Result?.ToList();

            Assert.IsNotNull(abbreviations);
            abbreviations.ForEach(i => Console.WriteLine("{0} | {1} | {2}", i.Value, i.ContentType, i.Definition));

            Assert.AreEqual(ExpectedNumberOfAbbreviations, abbreviations.Count);
        }
    }
}
