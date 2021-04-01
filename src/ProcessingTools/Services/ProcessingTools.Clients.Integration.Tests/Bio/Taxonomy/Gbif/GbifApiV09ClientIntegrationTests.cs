// <copyright file="GbifApiV09ClientIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Integration.Tests.Bio.Taxonomy.Gbif
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Clients.Bio.Taxonomy.Gbif;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json;
    using ProcessingTools.Services.Serialization;

    /// <summary>
    /// <see cref="GbifApiV09Client"/> integration tests.
    /// </summary>
    [TestFixture(Category = "Integration", TestOf = typeof(GbifApiV09Client), Author = "Bozhin Karaivanov")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1001:Types that own disposable fields should be disposable", Justification = "HttpClient")]
    public class GbifApiV09ClientIntegrationTests
    {
        private HttpClient httpClient;

        /// <summary>
        /// <see cref="OneTimeSetUpAttribute"/> method.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://api.gbif.org"),
            };
        }

        /// <summary>
        /// <see cref="OneTimeTearDownAttribute"/> method.
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            this.httpClient.Dispose();
        }

        /// <summary>
        /// <see cref="GbifApiV09Client"/>.GetDataPerNameAsync with valid taxon name should return valid result.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous integration test.</returns>
        [Test(TestOf = typeof(GbifApiV09Client), Author = "Bozhin Karaivanov", Description = "GbifApiV09Client.GetDataPerNameAsync with valid taxon name should return valid result.")]
        [Timeout(5000)]
        [Ignore("Integration test")]
        public async Task GbifApiV09Client_GetDataPerNameAsync_WithValidTaxonName_ShouldReturnValidResult()
        {
            // Arrange
            string scientificName = "Coleoptera";

            var clientFactoryMock = new Mock<IHttpClientFactory>();
            clientFactoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(this.httpClient);

            var deserializer = new NewtonsoftJsonDeserializer<GbifApiV09ResponseModel>();

            var client = new GbifApiV09Client(clientFactoryMock.Object, deserializer);

            // Act
            var response = await client.GetDataPerNameAsync(scientificName).ConfigureAwait(false);

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(scientificName, response.CanonicalName);
        }
    }
}
