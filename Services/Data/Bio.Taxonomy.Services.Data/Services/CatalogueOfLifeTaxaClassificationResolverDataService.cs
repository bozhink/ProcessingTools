namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Contracts;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Models;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Common.Constants;

    public class CatalogueOfLifeTaxaClassificationResolverDataService : TaxaInformationResolverDataServiceFactory<ITaxonClassification>, ICatalogueOfLifeTaxaClassificationResolverDataService
    {
        private ICatalogueOfLifeDataRequester requester;

        public CatalogueOfLifeTaxaClassificationResolverDataService(ICatalogueOfLifeDataRequester requester)
        {
            if (requester == null)
            {
                throw new ArgumentNullException(nameof(requester));
            }

            this.requester = requester;
        }

        protected override void Delay()
        {
            Thread.Sleep(ConcurrencyConstants.DefaultDelayTime);
        }

        protected override async Task ResolveScientificName(string scientificName, ConcurrentQueue<ITaxonClassification> taxaQueue)
        {
            var response = await this.requester.RequestData(scientificName);

            try
            {
                var matchingResults = new HashSet<Result>(response.Results
                    .Where(r => r.Name == scientificName));

                matchingResults
                    .Select(this.MapResultToClassification)
                    .ToList()
                    .ForEach(result => taxaQueue.Enqueue(result));
            }
            catch (ArgumentNullException)
            {
                // Linq queries failed.
                // There are no matching response items.
                // Do nothing.
            }
        }

        private ITaxonClassification MapResultToClassification(Result result)
        {
            return new TaxonClassificationServiceModel
            {
                ScientificName = result.Name,
                Rank = result.Rank?.ToLower(),
                Authority = result.Author,
                CanonicalName = result.AcceptedName?.Name,

                Aberration = this.GetClassificationItem(result, TaxonRankType.Aberration),
                Clade = this.GetClassificationItem(result, TaxonRankType.Clade),
                Class = this.GetClassificationItem(result, TaxonRankType.Class),
                Cohort = this.GetClassificationItem(result, TaxonRankType.Cohort),
                Division = this.GetClassificationItem(result, TaxonRankType.Division),
                Family = this.GetClassificationItem(result, TaxonRankType.Family),
                Form = this.GetClassificationItem(result, TaxonRankType.Form),
                Genus = this.GetClassificationItem(result, TaxonRankType.Genus),
                Infraclass = this.GetClassificationItem(result, TaxonRankType.Infraclass),
                Infradivision = this.GetClassificationItem(result, TaxonRankType.Infradivision),
                Infrakingdom = this.GetClassificationItem(result, TaxonRankType.Infrakingdom),
                Infraorder = this.GetClassificationItem(result, TaxonRankType.Infraorder),
                Infraphylum = this.GetClassificationItem(result, TaxonRankType.Infraphylum),
                Kingdom = this.GetClassificationItem(result, TaxonRankType.Kingdom),
                Order = this.GetClassificationItem(result, TaxonRankType.Order),
                Parvorder = this.GetClassificationItem(result, TaxonRankType.Parvorder),
                Phylum = this.GetClassificationItem(result, TaxonRankType.Phylum),
                Race = this.GetClassificationItem(result, TaxonRankType.Race),
                Section = this.GetClassificationItem(result, TaxonRankType.Section),
                Series = this.GetClassificationItem(result, TaxonRankType.Series),
                Species = this.GetClassificationItem(result, TaxonRankType.Species),
                Stage = this.GetClassificationItem(result, TaxonRankType.Stage),
                Subclade = this.GetClassificationItem(result, TaxonRankType.Subclade),
                Subclass = this.GetClassificationItem(result, TaxonRankType.Subclass),
                Subdivision = this.GetClassificationItem(result, TaxonRankType.Subdivision),
                Subfamily = this.GetClassificationItem(result, TaxonRankType.Subfamily),
                Subform = this.GetClassificationItem(result, TaxonRankType.Subform),
                Subgenus = this.GetClassificationItem(result, TaxonRankType.Subgenus),
                Subkingdom = this.GetClassificationItem(result, TaxonRankType.Subkingdom),
                Suborder = this.GetClassificationItem(result, TaxonRankType.Suborder),
                Subphylum = this.GetClassificationItem(result, TaxonRankType.Subphylum),
                Subsection = this.GetClassificationItem(result, TaxonRankType.Subsection),
                Subseries = this.GetClassificationItem(result, TaxonRankType.Subseries),
                Subspecies = this.GetClassificationItem(result, TaxonRankType.Subspecies),
                Subtribe = this.GetClassificationItem(result, TaxonRankType.Subtribe),
                Subvariety = this.GetClassificationItem(result, TaxonRankType.Subvariety),
                Superclass = this.GetClassificationItem(result, TaxonRankType.Superclass),
                Superfamily = this.GetClassificationItem(result, TaxonRankType.Superfamily),
                Supergroup = this.GetClassificationItem(result, TaxonRankType.Supergroup),
                Superkingdom = this.GetClassificationItem(result, TaxonRankType.Superkingdom),
                Superorder = this.GetClassificationItem(result, TaxonRankType.Superorder),
                Superphylum = this.GetClassificationItem(result, TaxonRankType.Superphylum),
                Superspecies = this.GetClassificationItem(result, TaxonRankType.Superspecies),
                Supertribe = this.GetClassificationItem(result, TaxonRankType.Supertribe),
                Tribe = this.GetClassificationItem(result, TaxonRankType.Tribe),
                Variety = this.GetClassificationItem(result, TaxonRankType.Variety),
            };
        }

        private string GetClassificationItem(Result result, TaxonRankType rank)
        {
            try
            {
                return result.Classification
                    .FirstOrDefault(c => string.Compare(c.Rank, rank.ToString(), true) == 0)
                    .Name;
            }
            catch
            {
                return null;
            }
        }
    }
}