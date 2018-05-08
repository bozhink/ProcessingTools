// <copyright file="AphiaNameServicePortTypeClientIntegrationTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.ConnectedServices.Tests.Integration.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Clients.Bio.Aphia.ServiceReference;

    /// <summary>
    /// <see cref="AphiaNameServicePortTypeClient"/> integration tests.
    /// </summary>
    [TestClass]
    public class AphiaNameServicePortTypeClientIntegrationTests
    {
        /// <summary>
        /// <see cref="AphiaNameServicePortTypeClient"/> GetAphiaRecords with valid parameters should work.
        /// </summary>
        [TestMethod]
        [Timeout(20000)]
        [Ignore(message: "Net dependent integration test")] // Net dependent integration test
        public void AphiaNameServicePortTypeClient_GetAphiaRecordsWithValidParameters_ShouldWork()
        {
            var client = new AphiaNameServicePortTypeClient();
            var records = client.getAphiaRecordsAsync(new getAphiaRecordsRequest("Anodontiglanis", true, true, false, 0)).Result.@return;

            Assert.IsTrue(records?.Length > 0, "Number of records should be greater than 0.");
            if (records != null)
            {
                AphiaRecord genusRecord = records[0];
                Assert.AreEqual("GENUS", genusRecord?.rank.ToUpperInvariant(), "Genus rank is not correct.");
                Assert.AreEqual("Anodontiglanis", genusRecord?.scientificname, "Scientific name is not correct.");
                Assert.AreEqual("Anodontiglanis", genusRecord?.valid_name, "Valid name is not correct.");
            }
        }
    }
}
