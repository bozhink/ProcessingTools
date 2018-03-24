// <copyright file="AphiaNameServiceIntegrationTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Tests.Integration.Tests.Bio.Taxonomy.Aphia
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.Aphia;

    /// <summary>
    /// <see cref="AphiaNameService"/> integration tests.
    /// </summary>
    [TestClass]
    public class AphiaNameServiceIntegrationTests
    {
        /// <summary>
        /// <see cref="AphiaNameService"/> GetAphiaRecords with valid parameters should work.
        /// </summary>
        [TestMethod]
        [Timeout(20000)]
        [Ignore(message: "Net dependent integration test")] // Net dependent integration test
        public void AphiaService_GetAphiaRecordsWithValidParameters_ShouldWork()
        {
            var service = new AphiaNameService();
            var records = service.getAphiaRecords("Anodontiglanis", true, true, false, 0);

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
