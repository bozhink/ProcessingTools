namespace ProcessingTools.Services.Data.Tests.Integration.Tests.Bio.Taxonomy
{
    using System.Linq;
    using NUnit.Framework;
    using ProcessingTools.Clients.Bio.Taxonomy.Gbif;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Net;
    using ProcessingTools.Services.Data.Services.Bio.Taxonomy;

    [TestFixture(Author = "Bozhin Karaivanov", Category = "Integration", TestOf = typeof(GbifTaxaClassificationResolver))]
    public class GbifTaxaClassificationResolverTests
    {
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(GbifTaxaClassificationResolver))]
        public void GbifTaxaClassificationResolver_DefaultConstructor_ShouldWork()
        {
            var connectorFactory = new NetConnectorFactory();
            var requester = new GbifApiV09DataRequester(connectorFactory);
            var service = new GbifTaxaClassificationResolver(requester);
            Assert.IsNotNull(service, "Service should not be null");
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(GbifTaxaClassificationResolver))]
        [Timeout(5000)]
        [Ignore(reason: "Integration test")]
        public void GbifTaxaClassificationResolver_Resolve_ShouldWork()
        {
            const string CanonicalName = "Coleoptera";
            const TaxonRankType Rank = TaxonRankType.Order;

            var connectorFactory = new NetConnectorFactory();
            var requester = new GbifApiV09DataRequester(connectorFactory);
            var service = new GbifTaxaClassificationResolver(requester);
            var response = service.Resolve(CanonicalName).Result;

            var defaultClassification = response.FirstOrDefault();

            Assert.AreEqual(CanonicalName, defaultClassification.CanonicalName, "CanonicalName should match.");
            Assert.AreEqual(Rank, defaultClassification.Rank, "Rank should match.");
        }
    }
}
