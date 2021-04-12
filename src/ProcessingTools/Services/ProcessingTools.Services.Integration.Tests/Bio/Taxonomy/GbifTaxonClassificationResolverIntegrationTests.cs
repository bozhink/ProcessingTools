// <copyright file="GbifTaxonClassificationResolverIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Integration.Tests.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Bio.Taxonomy.Common;
    using ProcessingTools.Bio.Taxonomy.External.GbifApiV09.Services;

    /// <summary>
    /// <see cref="GbifTaxonClassificationResolver"/> integration tests.
    /// </summary>
    [TestFixture(Category = "Integration", TestOf = typeof(GbifTaxonClassificationResolver))]
    public class GbifTaxonClassificationResolverIntegrationTests
    {
        /// <summary>
        /// <see cref="GbifTaxonClassificationResolver"/> default constructor should work.
        /// </summary>
        [Test(TestOf = typeof(GbifTaxonClassificationResolver), Description = "GbifTaxonClassificationResolver default constructor should work.")]
        public void GbifTaxonClassificationResolver_DefaultConstructor_ShouldWork()
        {
            // Arrange
            var factoryMock = new Mock<IHttpClientFactory>();
            _ = factoryMock.Setup(f => f.CreateClient())
                .Returns(new HttpClient { BaseAddress = new Uri("http://api.gbif.org") });

            var loggerMock = new Mock<ILogger<GbifApiV09Client>>();

            var requester = new GbifApiV09Client(factoryMock.Object, loggerMock.Object);
            var service = new GbifTaxonClassificationResolver(requester);

            // Assert
            Assert.IsNotNull(service);
        }

        /// <summary>
        /// <see cref="GbifTaxonClassificationResolver"/> resolve should work.
        /// </summary>
        [Test(TestOf = typeof(GbifTaxonClassificationResolver), Description = "GbifTaxonClassificationResolver resolve should work.")]
        [MaxTime(100000)]
        public void GbifTaxonClassificationResolver_Resolve_ShouldWork()
        {
            // Arrange
            const string CanonicalName = "Coleoptera";
            const TaxonRankType Rank = TaxonRankType.Order;

            var factoryMock = new Mock<IHttpClientFactory>();
            _ = factoryMock.Setup(f => f.CreateClient())
                .Returns(new HttpClient { BaseAddress = new Uri("http://api.gbif.org") });

            var loggerMock = new Mock<ILogger<GbifApiV09Client>>();

            var requester = new GbifApiV09Client(factoryMock.Object, loggerMock.Object);
            var service = new GbifTaxonClassificationResolver(requester);

            // Act
            var response = service.ResolveAsync(new[] { CanonicalName }).Result;

            var defaultClassification = response.FirstOrDefault();

            // Assert
            Assert.AreEqual(CanonicalName, defaultClassification.CanonicalName);
            Assert.AreEqual(Rank, defaultClassification.Rank);
        }
    }
}
