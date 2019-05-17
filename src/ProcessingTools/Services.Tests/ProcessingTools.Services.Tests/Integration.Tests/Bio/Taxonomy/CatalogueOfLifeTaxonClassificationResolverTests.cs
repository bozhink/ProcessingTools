namespace ProcessingTools.Services.Tests.Integration.Tests.Bio.Taxonomy
{
    using System.Linq;
    using NUnit.Framework;
    using ProcessingTools.Clients.Bio.Taxonomy.CatalogueOfLife;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Net;
    using ProcessingTools.Services.Bio.Taxonomy;

    [TestFixture(Author = "Bozhin Karaivanov", Category = "Integration", TestOf = typeof(CatalogueOfLifeTaxonClassificationResolver))]
    public class CatalogueOfLifeTaxonClassificationResolverTests
    {
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(CatalogueOfLifeTaxonClassificationResolver))]
        public void CatalogueOfLifeTaxonClassificationResolver_DefaultConstructor_ShouldWork()
        {
            var requester = new CatalogueOfLifeDataRequester(new HttpRequesterFactory());
            var service = new CatalogueOfLifeTaxonClassificationResolver(requester);
            Assert.IsNotNull(service, "Service should not be null");
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(CatalogueOfLifeTaxonClassificationResolver))]
        [MaxTime(100000)]
        [Ignore(reason: "Integration test")]
        public void CatalogueOfLifeTaxonClassificationResolver_Resolve_ShouldWork()
        {
            const string ScientificName = "Coleoptera";
            const TaxonRankType Rank = TaxonRankType.Order;

            var requester = new CatalogueOfLifeDataRequester(new HttpRequesterFactory());
            var service = new CatalogueOfLifeTaxonClassificationResolver(requester);
            var response = service.ResolveAsync(new[] { ScientificName }).Result;

            Assert.IsNotNull(response, "Response should not be null.");

            var defaultClassification = response.SingleOrDefault();

            Assert.AreEqual(ScientificName, defaultClassification.ScientificName, "ScientificName should match.");
            Assert.AreEqual(Rank, defaultClassification.Rank, "Rank should match.");
        }
    }
}
