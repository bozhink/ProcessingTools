// <copyright file="GbifTaxonClassificationResolverTests.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Tests.Integration.Tests.Bio.Taxonomy
{
    using System.Linq;
    using NUnit.Framework;
    using ProcessingTools.Clients.Bio.Taxonomy.Gbif;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Services.Bio.Taxonomy;
    using ProcessingTools.Services.Net;

    [TestFixture(Author = "Bozhin Karaivanov", Category = "Integration", TestOf = typeof(GbifTaxonClassificationResolver))]
    public class GbifTaxonClassificationResolverTests
    {
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(GbifTaxonClassificationResolver))]
        public void GbifTaxonClassificationResolver_DefaultConstructor_ShouldWork()
        {
            var httpRequester = new HttpRequester();
            var requester = new GbifApiV09DataRequester(httpRequester);
            var service = new GbifTaxonClassificationResolver(requester);
            Assert.IsNotNull(service, "Service should not be null");
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(GbifTaxonClassificationResolver))]
        [MaxTime(100000)]
        [Ignore(reason: "Integration test")]
        public void GbifTaxonClassificationResolver_Resolve_ShouldWork()
        {
            const string CanonicalName = "Coleoptera";
            const TaxonRankType Rank = TaxonRankType.Order;

            var httpRequester = new HttpRequester();
            var requester = new GbifApiV09DataRequester(httpRequester);
            var service = new GbifTaxonClassificationResolver(requester);
            var response = service.ResolveAsync(new[] { CanonicalName }).Result;

            var defaultClassification = response.FirstOrDefault();

            Assert.AreEqual(CanonicalName, defaultClassification.CanonicalName, "CanonicalName should match.");
            Assert.AreEqual(Rank, defaultClassification.Rank, "Rank should match.");
        }
    }
}
