namespace ProcessingTools.Bio.Taxonomy.Services.Data.Tests
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife;
    using Services;

    [TestClass]
    public class CatalogueOfLifeTaxaClassificationDataServiceTests
    {
        [TestMethod]
        public void CatalogueOfLifeTaxaClassificationDataService_DefaultConstructor_ShouldWork()
        {
            var requester = new CatalogueOfLifeDataRequester();
            var service = new CatalogueOfLifeTaxaClassificationDataService(requester);
            Assert.IsNotNull(service, "Service should not be null");
        }

        [TestMethod]
        public void CatalogueOfLifeTaxaClassificationDataService_Resolve_ShouldWork()
        {
            const string ScientificName = "Coleoptera";
            const string Rank = "order";

            var requester = new CatalogueOfLifeDataRequester();
            var service = new CatalogueOfLifeTaxaClassificationDataService(requester);
            var response = service.Resolve(ScientificName);

            var defaultClassification = response.FirstOrDefault();

            Assert.AreEqual(ScientificName, defaultClassification.ScientificName, "ScientificName should match.");
            Assert.AreEqual(Rank, defaultClassification.Rank, "Rank should match.");
        }
    }
}