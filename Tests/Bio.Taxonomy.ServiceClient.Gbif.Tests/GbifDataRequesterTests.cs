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
            string scientificName = "Coleoptera";
            var result = GbifDataRequester.SearchGbif(scientificName).Result;

            Assert.IsNotNull(result, "Result schould not be null.");
            Assert.AreEqual(scientificName, result.CanonicalName, "CanonicalName schould match.");
        }
    }
}