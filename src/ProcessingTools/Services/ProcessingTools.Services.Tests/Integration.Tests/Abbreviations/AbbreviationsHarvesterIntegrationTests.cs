// <copyright file="AbbreviationsHarvesterIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Tests.Integration.Tests.Abbreviations
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Services.Abbreviations;

    /// <summary>
    /// <see cref="AbbreviationsHarvester"/> integration tests.
    /// </summary>
    [TestClass]
    public class AbbreviationsHarvesterIntegrationTests
    {
        /// <summary>
        /// Gets or sets the <see cref="TestContext"/>.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// <see cref="AbbreviationsHarvester"/> harvest sample document should succeed.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        public void AbbreviationsHarvester_HarvestSampleDocument_ShouldSucceed()
        {
            Assert.Fail("Test needs revision + re-implementation of the SUT.");

            ////// Arrange
            ////const int ExpectedNumberOfAbbreviations = 22;

            ////var xmlFileName = Path.Combine("Samples", "article -with-abbrev.xml");
            ////XmlDocument document = new XmlDocument
            ////{
            ////    PreserveWhitespace = true,
            ////};

            ////document.Load(xmlFileName);

            ////var contextWrapper = new XmlContextWrapper();
            ////var harvesterCore = new EnumerableXmlHarvesterCore<IAbbreviationModel>(contextWrapper);

            ////var deserializer = new XmlDeserializer();
            ////var serializer = new XmlTransformDeserializer(deserializer);

            ////var xqueryCache = new XQueryTransformCache();
            ////var transformer = new XQueryTransformer(
            ////    AppSettings.AbbreviationsXQueryFileName,
            ////    xqueryCache);
            ////var transformerFactoryMock = new Mock<IAbbreviationsTransformerFactory>();
            ////transformerFactoryMock
            ////    .Setup(f => f.GetAbbreviationsTransformer())
            ////    .Returns(transformer);

            ////var harvester = new AbbreviationsHarvester(harvesterCore, serializer, transformerFactoryMock.Object);

            ////// Act
            ////var abbreviations = harvester.HarvestAsync(document.DocumentElement).Result?.ToList();

            ////Assert.IsNotNull(abbreviations);
            ////abbreviations?.ForEach(i => this.TestContext.WriteLine("{0} | {1} | {2}", i.Value, i.ContentType, i.Definition));

            ////Assert.AreEqual(ExpectedNumberOfAbbreviations, abbreviations?.Count);
        }
    }
}
