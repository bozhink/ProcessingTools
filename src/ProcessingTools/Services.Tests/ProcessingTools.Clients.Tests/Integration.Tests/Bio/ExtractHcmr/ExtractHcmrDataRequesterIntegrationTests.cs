// <copyright file="ExtractHcmrDataRequesterIntegrationTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Tests.Integration.Tests.Bio.ExtractHcmr
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Clients.Bio.ExtractHcmr;
    using ProcessingTools.Net;

    /// <summary>
    /// <see cref="ExtractHcmrDataRequester"/> integration tests.
    /// </summary>
    [TestClass]
    public class ExtractHcmrDataRequesterIntegrationTests
    {
        /// <summary>
        /// <see cref="ExtractHcmrDataRequester"/> request data with valid content should work.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        [Ignore(message: "Net dependent integration test")] // Net dependent integration test
        public void ExtractHcmrDataRequester_RequestData_WithValidContent_ShouldWork()
        {
            const string Content = "Both samples were dominated by Zetaproteobacteria Fe oxidizers. This group was most abundant at Volcano 1, where sediments were richer in Fe and contained more crystalline forms of Fe oxides.";

            var requester = new ExtractHcmrDataRequester(new NetConnectorFactory());
            var response = requester.RequestDataAsync(Content)?.Result;

            Assert.IsNotNull(response, "Response should not be null.");

            Assert.IsNotNull(response?.Items, "Response items should not be null");

            Assert.IsTrue(response?.Items.Length > 0, "The number of response items should be greater than 0.");

            var volcanoItem = response?.Items.FirstOrDefault(i => i.Name == "Volcano");

            Assert.IsNotNull(volcanoItem, "Volcano item should not be null");

            Assert.AreEqual(
                "ENVO:00000247",
                volcanoItem?.Entities.FirstOrDefault().Identifier,
                "Volcano identifier should match.");

            ////// This is valid only if entity type = -2 is used.
            ////var zetaproteobacteriaItem = response.Items.FirstOrDefault(i => i.Name == "Zetaproteobacteria");

            ////Assert.IsNotNull(zetaproteobacteriaItem, "Zetaproteobacteria item should not be null");

            ////Assert.AreEqual(
            ////    "580370",
            ////    zetaproteobacteriaItem.Entities.FirstOrDefault().Identifier,
            ////    "Zetaproteobacteria identifier should match.");

            var sedimentsItem = response?.Items.FirstOrDefault(i => i.Name == "sediments");

            Assert.IsNotNull(sedimentsItem, "Sediments item should not be null.");

            Assert.AreEqual(
                "ENVO:00002007",
                sedimentsItem?.Entities.FirstOrDefault().Identifier,
                "Sediments identifier should match.");
        }
    }
}
