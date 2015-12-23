namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Infrastructure.Concurrency;
    using Models;
    using ServiceClient.CatalogueOfLife.Contracts;
    using ServiceClient.CatalogueOfLife.Models;
    using Taxonomy.Contracts;
    using Types;

    public class CatalogueOfLifeTaxaClassificationDataService : TaxaDataServiceFactory<ITaxonClassification>, ICatalogueOfLifeTaxaClassificationDataService
    {
        private ICatalogueOfLifeDataRequester requester;

        public CatalogueOfLifeTaxaClassificationDataService(ICatalogueOfLifeDataRequester requester)
        {
            this.requester = requester;
        }

        protected override void Delay()
        {
            Delayer.Delay();
        }

        protected override void ResolveScientificName(string scientificName, ConcurrentQueue<ITaxonClassification> taxaQueue)
        {
            var response = requester.RequestData(scientificName).Result;

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
            return new TaxonClassificationDataServiceResponseModel
            {
                ScientificName = result.Name,
                Rank = result.Rank?.ToLower(),
                Authority = result.Author,
                CanonicalName = result.AcceptedName?.Name,

                Aberration = this.GetClassificationItem(result, TaxonRanksType.Aberration),
                Clade = this.GetClassificationItem(result, TaxonRanksType.Clade),
                Class = this.GetClassificationItem(result, TaxonRanksType.Class),
                Cohort = this.GetClassificationItem(result, TaxonRanksType.Cohort),
                Division = this.GetClassificationItem(result, TaxonRanksType.Division),
                Family = this.GetClassificationItem(result, TaxonRanksType.Family),
                Form = this.GetClassificationItem(result, TaxonRanksType.Form),
                Genus = this.GetClassificationItem(result, TaxonRanksType.Genus),
                Infraclass = this.GetClassificationItem(result, TaxonRanksType.Infraclass),
                Infradivision = this.GetClassificationItem(result, TaxonRanksType.Infradivision),
                Infrakingdom = this.GetClassificationItem(result, TaxonRanksType.Infrakingdom),
                Infraorder = this.GetClassificationItem(result, TaxonRanksType.Infraorder),
                Infraphylum = this.GetClassificationItem(result, TaxonRanksType.Infraphylum),
                Kingdom = this.GetClassificationItem(result, TaxonRanksType.Kingdom),
                Order = this.GetClassificationItem(result, TaxonRanksType.Order),
                Parvorder = this.GetClassificationItem(result, TaxonRanksType.Parvorder),
                Phylum = this.GetClassificationItem(result, TaxonRanksType.Phylum),
                Race = this.GetClassificationItem(result, TaxonRanksType.Race),
                Section = this.GetClassificationItem(result, TaxonRanksType.Section),
                Series = this.GetClassificationItem(result, TaxonRanksType.Series),
                Species = this.GetClassificationItem(result, TaxonRanksType.Species),
                Stage = this.GetClassificationItem(result, TaxonRanksType.Stage),
                Subclade = this.GetClassificationItem(result, TaxonRanksType.Subclade),
                Subclass = this.GetClassificationItem(result, TaxonRanksType.Subclass),
                Subdivision = this.GetClassificationItem(result, TaxonRanksType.Subdivision),
                Subfamily = this.GetClassificationItem(result, TaxonRanksType.Subfamily),
                Subform = this.GetClassificationItem(result, TaxonRanksType.Subform),
                Subgenus = this.GetClassificationItem(result, TaxonRanksType.Subgenus),
                Subkingdom = this.GetClassificationItem(result, TaxonRanksType.Subkingdom),
                Suborder = this.GetClassificationItem(result, TaxonRanksType.Suborder),
                Subphylum = this.GetClassificationItem(result, TaxonRanksType.Subphylum),
                Subsection = this.GetClassificationItem(result, TaxonRanksType.Subsection),
                Subseries = this.GetClassificationItem(result, TaxonRanksType.Subseries),
                Subspecies = this.GetClassificationItem(result, TaxonRanksType.Subspecies),
                Subtribe = this.GetClassificationItem(result, TaxonRanksType.Subtribe),
                Subvariety = this.GetClassificationItem(result, TaxonRanksType.Subvariety),
                Superclass = this.GetClassificationItem(result, TaxonRanksType.Superclass),
                Superfamily = this.GetClassificationItem(result, TaxonRanksType.Superfamily),
                Supergroup = this.GetClassificationItem(result, TaxonRanksType.Supergroup),
                Superkingdom = this.GetClassificationItem(result, TaxonRanksType.Superkingdom),
                Superorder = this.GetClassificationItem(result, TaxonRanksType.Superorder),
                Superphylum = this.GetClassificationItem(result, TaxonRanksType.Superphylum),
                Superspecies = this.GetClassificationItem(result, TaxonRanksType.Superspecies),
                Supertribe = this.GetClassificationItem(result, TaxonRanksType.Supertribe),
                Tribe = this.GetClassificationItem(result, TaxonRanksType.Tribe),
                Variety = this.GetClassificationItem(result, TaxonRanksType.Variety),
            };
        }

        private string GetClassificationItem(Result result, TaxonRanksType rank)
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