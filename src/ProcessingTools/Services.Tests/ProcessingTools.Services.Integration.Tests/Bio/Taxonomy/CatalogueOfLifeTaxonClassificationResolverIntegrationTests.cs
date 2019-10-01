// <copyright file="CatalogueOfLifeTaxonClassificationResolverIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Integration.Tests.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Clients.Bio.Taxonomy.CatalogueOfLife;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Services.Bio.Taxonomy;
    using ProcessingTools.Services.Serialization;

    /// <summary>
    /// <see cref="CatalogueOfLifeTaxonClassificationResolver"/> integration tests.
    /// </summary>
    [TestFixture(Author = "Bozhin Karaivanov", Category = "Integration", TestOf = typeof(CatalogueOfLifeTaxonClassificationResolver))]
    public class CatalogueOfLifeTaxonClassificationResolverIntegrationTests
    {
        /// <summary>
        /// <see cref="CatalogueOfLifeTaxonClassificationResolver"/> default constructor should work.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(CatalogueOfLifeTaxonClassificationResolver), Description = "CatalogueOfLifeTaxonClassificationResolver  default constructor should work.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "HttpClient")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "HttpClient")]
        public void CatalogueOfLifeTaxonClassificationResolver_DefaultConstructor_ShouldWork()
        {
            // Arrange
            var factoryMock = new Mock<IHttpClientFactory>();
            _ = factoryMock.Setup(f => f.CreateClient())
                .Returns(new HttpClient { BaseAddress = new Uri("http://www.catalogueoflife.org") });

            var deserializer = new XmlDeserializer<CatalogueOfLifeApiServiceXmlResponseModel>();

            var requester = new CatalogueOfLifeWebserviceClient(factoryMock.Object, deserializer);
            var service = new CatalogueOfLifeTaxonClassificationResolver(requester);

            // Assert
            Assert.IsNotNull(service);
        }

        /// <summary>
        /// <see cref="CatalogueOfLifeTaxonClassificationResolver"/> resolve should work.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(CatalogueOfLifeTaxonClassificationResolver), Description = "CatalogueOfLifeTaxonClassificationResolver resolve should work.")]
        [MaxTime(100000)]
        [Ignore(reason: "Integration test")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "HttpClient")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "HttpClient")]
        public void CatalogueOfLifeTaxonClassificationResolver_Resolve_ShouldWork()
        {
            // Arrange
            const string ScientificName = "Coleoptera";
            const TaxonRankType Rank = TaxonRankType.Order;

            var factoryMock = new Mock<IHttpClientFactory>();
            _ = factoryMock.Setup(f => f.CreateClient())
                .Returns(new HttpClient { BaseAddress = new Uri("http://www.catalogueoflife.org") });

            var deserializer = new XmlDeserializer<CatalogueOfLifeApiServiceXmlResponseModel>();

            var requester = new CatalogueOfLifeWebserviceClient(factoryMock.Object, deserializer);
            var service = new CatalogueOfLifeTaxonClassificationResolver(requester);

            // Act
            var response = service.ResolveAsync(new[] { ScientificName }).Result;

            // Assert
            Assert.IsNotNull(response);

            var defaultClassification = response.SingleOrDefault();

            Assert.AreEqual(ScientificName, defaultClassification.ScientificName);
            Assert.AreEqual(Rank, defaultClassification.Rank);
        }
    }
}
