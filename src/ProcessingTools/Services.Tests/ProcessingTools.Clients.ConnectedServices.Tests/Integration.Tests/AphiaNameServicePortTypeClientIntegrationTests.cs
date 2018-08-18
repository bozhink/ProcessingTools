// <copyright file="AphiaNameServicePortTypeClientIntegrationTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.ConnectedServices.Tests.Integration.Tests
{
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Clients.ConnectedServices.Bio.Aphia;

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
        public async Task AphiaNameServicePortTypeClient_GetAphiaRecordsWithValidParameters_ShouldWork()
        {
            // Arrange
            var request = new getAphiaRecordsRequest("Anodontiglanis", true, true, false, 0);
            var client = new AphiaNameServicePortTypeClient();

            // Act

            await client.OpenAsync().ConfigureAwait(false);

            var task = client.getAphiaRecordsAsync(request);
            var result = await task.ConfigureAwait(false);
            var records = result.@return;

            await client.CloseAsync().ConfigureAwait(false);

            // Assert
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
