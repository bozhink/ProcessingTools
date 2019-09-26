// <copyright file="CatalogueOfLifeTaxonClassificationResolverTests.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Tests.Integration.Tests.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Clients.Bio.Taxonomy.CatalogueOfLife;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Services.Bio.Taxonomy;
    using ProcessingTools.Services.Net;

    [TestFixture(Author = "Bozhin Karaivanov", Category = "Integration", TestOf = typeof(CatalogueOfLifeTaxonClassificationResolver))]
    public class CatalogueOfLifeTaxonClassificationResolverTests
    {
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(CatalogueOfLifeTaxonClassificationResolver))]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "HttpClient")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "HttpClient")]
        public void CatalogueOfLifeTaxonClassificationResolver_DefaultConstructor_ShouldWork()
        {
            // Arrange
            var factoryMock = new Mock<IHttpClientFactory>();
            _ = factoryMock.Setup(f => f.CreateClient())
                .Returns(new HttpClient { BaseAddress = new Uri("http://www.catalogueoflife.org") });

            var requester = new CatalogueOfLifeWebserviceClient(factoryMock.Object);
            var service = new CatalogueOfLifeTaxonClassificationResolver(requester);

            // Assert
            Assert.IsNotNull(service);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(CatalogueOfLifeTaxonClassificationResolver))]
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

            var requester = new CatalogueOfLifeWebserviceClient(factoryMock.Object);
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
