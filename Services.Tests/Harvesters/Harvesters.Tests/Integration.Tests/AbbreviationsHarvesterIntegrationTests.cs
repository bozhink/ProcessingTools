namespace ProcessingTools.Harvesters.Tests.Integration.Tests
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Xml;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Harvesters.Contracts.Transformers;
    using ProcessingTools.Harvesters.Harvesters.Abbreviations;
    using ProcessingTools.Harvesters.Transformers;
    using ProcessingTools.Serialization.Serializers;
    using ProcessingTools.Xml.Cache;
    using ProcessingTools.Xml.Providers;
    using ProcessingTools.Xml.Serialization;

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
            var transformProvider = new GetAbbreviationsXQueryTransformProvider(xqueryCache);
            var transformer = new GetAbbreviationsTransformer(transformProvider);

            var harvester = new AbbreviationsHarvester(contextWrapperProvider, serializer, transformer);

            // Act
            var abbreviations = harvester.Harvest(document.DocumentElement).Result?.ToList();

            Assert.IsNotNull(abbreviations);
            abbreviations.ForEach(i => Console.WriteLine("{0} | {1} | {2}", i.Value, i.ContentType, i.Definition));

            Assert.AreEqual(ExpectedNumberOfAbbreviations, abbreviations.Count);
        }
    }
}
