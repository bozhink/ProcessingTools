namespace ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Clients.Bio.Taxonomy.Gbif;
    using ProcessingTools.Net;

    [TestClass]
    public class GbifDataRequesterTests
    {
        [TestMethod]
        [Timeout(5000)]
        [Ignore]
        public void GbifDataRequester_SearchGbif_WithValidTaxonName_ShouldReturnResult()
        {
            const string ScientificName = "Coleoptera";

            var requester = new GbifApiV09DataRequester(new NetConnectorFactory());
            var result = requester.RequestDataAsync(ScientificName).Result;

            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(ScientificName, result.CanonicalName, "CanonicalName should match.");
        }
    }
}