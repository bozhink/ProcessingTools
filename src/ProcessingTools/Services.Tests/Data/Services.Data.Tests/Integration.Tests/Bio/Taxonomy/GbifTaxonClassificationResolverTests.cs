namespace ProcessingTools.Services.Data.Tests.Integration.Tests.Bio.Taxonomy
{
    using System.Linq;
    using NUnit.Framework;
    using ProcessingTools.Clients.Bio.Taxonomy.Gbif;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Net;
    using ProcessingTools.Services.Bio.Taxonomy;

    [TestFixture(Author = "Bozhin Karaivanov", Category = "Integration", TestOf = typeof(GbifTaxonClassificationResolver))]
    public class GbifTaxonClassificationResolverTests
    {
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(GbifTaxonClassificationResolver))]
        public void GbifTaxonClassificationResolver_DefaultConstructor_ShouldWork()
        {
            var connectorFactory = new NetConnectorFactory();
            var requester = new GbifApiV09DataRequester(connectorFactory);
            var service = new GbifTaxonClassificationResolver(requester);
            Assert.IsNotNull(service, "Service should not be null");
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(GbifTaxonClassificationResolver))]
        [Timeout(5000)]
        [Ignore(reason: "Integration test")]
        public void GbifTaxonClassificationResolver_Resolve_ShouldWork()
        {
            const string CanonicalName = "Coleoptera";
            const TaxonRankType Rank = TaxonRankType.Order;

            var connectorFactory = new NetConnectorFactory();
            var requester = new GbifApiV09DataRequester(connectorFactory);
            var service = new GbifTaxonClassificationResolver(requester);
            var response = service.ResolveAsync(CanonicalName).Result;

            var defaultClassification = response.FirstOrDefault();

            Assert.AreEqual(CanonicalName, defaultClassification.CanonicalName, "CanonicalName should match.");
            Assert.AreEqual(Rank, defaultClassification.Rank, "Rank should match.");
        }
    }
}
