// <copyright file="GbifTaxonClassificationResolverIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Integration.Tests.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Clients.Bio.Taxonomy.Gbif;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Services.Bio.Taxonomy;
    using ProcessingTools.Services.Serialization;

    /// <summary>
    /// <see cref="GbifTaxonClassificationResolver"/> integration tests.
    /// </summary>
    [TestFixture(Author = "Bozhin Karaivanov", Category = "Integration", TestOf = typeof(GbifTaxonClassificationResolver))]
    public class GbifTaxonClassificationResolverIntegrationTests
    {
        /// <summary>
        /// <see cref="GbifTaxonClassificationResolver"/> default constructor should work.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(GbifTaxonClassificationResolver), Description = "GbifTaxonClassificationResolver default constructor should work.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "HttpClient")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "HttpClient")]
        public void GbifTaxonClassificationResolver_DefaultConstructor_ShouldWork()
        {
            // Arrange
            var factoryMock = new Mock<IHttpClientFactory>();
            _ = factoryMock.Setup(f => f.CreateClient())
                .Returns(new HttpClient { BaseAddress = new Uri("http://api.gbif.org") });

            var deserializer = new NewtonsoftJsonDeserializer<GbifApiV09ResponseModel>();

            var requester = new GbifApiV09Client(factoryMock.Object, deserializer);
            var service = new GbifTaxonClassificationResolver(requester);

            // Assert
            Assert.IsNotNull(service);
        }

        /// <summary>
        /// <see cref="GbifTaxonClassificationResolver"/> resolve should work.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(GbifTaxonClassificationResolver), Description = "GbifTaxonClassificationResolver resolve should work.")]
        [MaxTime(100000)]
        [Ignore(reason: "Integration test")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "HttpClient")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "HttpClient")]
        public void GbifTaxonClassificationResolver_Resolve_ShouldWork()
        {
            // Arrange
            const string CanonicalName = "Coleoptera";
            const TaxonRankType Rank = TaxonRankType.Order;

            var factoryMock = new Mock<IHttpClientFactory>();
            _ = factoryMock.Setup(f => f.CreateClient())
                .Returns(new HttpClient { BaseAddress = new Uri("http://api.gbif.org") });

            var deserializer = new NewtonsoftJsonDeserializer<GbifApiV09ResponseModel>();

            var requester = new GbifApiV09Client(factoryMock.Object, deserializer);
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
