namespace ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GbifDataRequesterTests
    {
        [TestMethod]
        [Ignore]
        public void GbifDataRequester_SearchGbif_WithValidTaxonName_ShouldReturnResult()
        {
            const string ScientificName = "Coleoptera";

            var requester = new GbifDataRequester();
            var result = requester.RequestData(ScientificName)?.Result;

            Assert.IsNotNull(result, "Result schould not be null.");
            Assert.AreEqual(ScientificName, result.CanonicalName, "CanonicalName schould match.");
        }
    }
}