namespace ProcessingTools.Bio.Taxonomy.Services.Data.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GbifTaxaClassificationDataServiceTests
    {
        [TestMethod]
        public void GbifTaxaClassificationDataService_DefaultConstructor_ShouldWork()
        {
            var service = new GbifTaxaClassificationDataService();
            Assert.IsNotNull(service, "Service should not be null");
        }

        [TestMethod]
        [Ignore]
        public void GbifTaxaClassificationDataService_Resolve_ShouldWork()
        {
            const string CanonicalName = "Coleoptera";
            const string Rank = "order";

            var service = new GbifTaxaClassificationDataService();
            var response = service.Resolve(CanonicalName);

            var defaultClassification = response.FirstOrDefault();

            Assert.AreEqual(CanonicalName, defaultClassification.CanonicalName, "CanonicalName should match.");
            Assert.AreEqual(Rank, defaultClassification.Rank, "Rank schould match.");
        }
    }
}