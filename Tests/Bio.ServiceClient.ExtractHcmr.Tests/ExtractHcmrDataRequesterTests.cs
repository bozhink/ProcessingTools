namespace ProcessingTools.Bio.ServiceClient.ExtractHcmr.Tests
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExtractHcmrDataRequesterTests
    {
        [TestMethod]
        [Ignore]
        public void ExtractHcmrDataRequester_RequestData_WithValidContent_SchouldWork()
        {
            const string Content = "Both samples were dominated by Zetaproteobacteria Fe oxidizers. This group was most abundant at Volcano 1, where sediments were richer in Fe and contained more crystalline forms of Fe oxides.";

            var requester = new ExtractHcmrDataRequester();
            var response = requester.RequestData(Content)?.Result;

            Assert.IsNotNull(response, "Response schould not be null.");

            Assert.IsNotNull(response.Items, "Response items schould not be null");

            Assert.IsTrue(response.Items.Length > 0, "The number of response items schould be greater than 0.");

            var volcanoItem = response.Items.FirstOrDefault(i => i.Name == "Volcano");

            Assert.IsNotNull(volcanoItem, "Volcano item schould not be null");

            Assert.AreEqual(
                "ENVO:00000247",
                volcanoItem.Entities.FirstOrDefault().Identifier,
                "Volcano identifier schould match.");

            ////// This is valid only if entity type = -2 is used.
            ////var zetaproteobacteriaItem = response.Items.FirstOrDefault(i => i.Name == "Zetaproteobacteria");

            ////Assert.IsNotNull(zetaproteobacteriaItem, "Zetaproteobacteria item schould not be null");

            ////Assert.AreEqual(
            ////    "580370",
            ////    zetaproteobacteriaItem.Entities.FirstOrDefault().Identifier,
            ////    "Zetaproteobacteria identifier schould match.");

            var sedimentsItem = response.Items.FirstOrDefault(i => i.Name == "sediments");

            Assert.IsNotNull(sedimentsItem, "Sediments item should not be null.");

            Assert.AreEqual(
                "ENVO:00002007",
                sedimentsItem.Entities.FirstOrDefault().Identifier,
                "Sediments identifier schould match.");
        }
    }
}