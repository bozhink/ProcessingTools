// <copyright file="GbifApiV09DataRequesterIntegrationTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Tests.Integration.Tests.Bio.Taxonomy.Gbif
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Clients.Bio.Taxonomy.Gbif;
    using ProcessingTools.Net;

    /// <summary>
    /// <see cref="GbifApiV09DataRequester"/> integration tests.
    /// </summary>
    [TestClass]
    public class GbifApiV09DataRequesterIntegrationTests
    {
        /// <summary>
        /// <see cref="GbifApiV09DataRequester"/> RequestDataAsync with valid taxon name should return result.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        [Ignore(message: "Net dependent integration test")] // Net dependent integration test
        public void GbifApiV09DataRequester_RequestDataAsync_WithValidTaxonName_ShouldReturnResult()
        {
            const string ScientificName = "Coleoptera";

            var requester = new GbifApiV09DataRequester(new NetConnectorFactory());
            var result = requester.RequestDataAsync(ScientificName).Result;

            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(ScientificName, result.CanonicalName, "CanonicalName should match.");
        }
    }
}
