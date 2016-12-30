namespace ProcessingTools.Services.Data.Tests.Integration.Tests.Bio.Taxonomy
{
    using System.Linq;
    using NUnit.Framework;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Net.Factories;
    using ProcessingTools.Services.Data.Services.Bio.Taxonomy;

    [TestFixture(Author = "Bozhin Karaivanov", Category = "Integration", TestOf = typeof(CatalogueOfLifeTaxaClassificationResolver))]
    public class CatalogueOfLifeTaxaClassificationResolverTests
    {
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(CatalogueOfLifeTaxaClassificationResolver))]
        public void CatalogueOfLifeTaxaClassificationResolver_DefaultConstructor_ShouldWork()
        {
            var requester = new CatalogueOfLifeDataRequester(new NetConnectorFactory());
            var service = new CatalogueOfLifeTaxaClassificationResolver(requester);
            Assert.IsNotNull(service, "Service should not be null");
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(CatalogueOfLifeTaxaClassificationResolver))]
        [Timeout(10000)]
        [Ignore(reason: "Integration test")]
        public void CatalogueOfLifeTaxaClassificationDataService_Resolve_ShouldWork()
        {
            const string ScientificName = "Coleoptera";
            const TaxonRankType Rank = TaxonRankType.Order;

            var requester = new CatalogueOfLifeDataRequester(new NetConnectorFactory());
            var service = new CatalogueOfLifeTaxaClassificationResolver(requester);
            var response = service.Resolve(ScientificName).Result;

            Assert.IsNotNull(response, "Response should not be null.");

            var defaultClassification = response.SingleOrDefault();

            Assert.AreEqual(ScientificName, defaultClassification.ScientificName, "ScientificName should match.");
            Assert.AreEqual(Rank, defaultClassification.Rank, "Rank should match.");
        }
    }
}
