namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Abstractions.Bio.Taxonomy;
    using Contracts.Bio.Taxonomy;
    using Models.Bio.Taxonomy;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif.Contracts;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif.Models.Contracts;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;

    public class GbifTaxaClassificationResolver : AbstractTaxaInformationResolver<ITaxonClassification>, IGbifTaxaClassificationResolver
    {
        private readonly IGbifApiV09DataRequester requester;

        public GbifTaxaClassificationResolver(IGbifApiV09DataRequester requester)
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

        protected override async Task<IEnumerable<ITaxonClassification>> ResolveScientificName(string scientificName)
        {
            var result = new HashSet<ITaxonClassification>();

            var response = await this.requester.RequestData(scientificName);

            if ((response != null) &&
                (!string.IsNullOrWhiteSpace(response.CanonicalName) ||
                 !string.IsNullOrWhiteSpace(response.ScientificName)))
            {
                if (response.CanonicalName.Equals(scientificName) ||
                    response.ScientificName.Contains(scientificName))
                {
                    result.Add(this.MapGbifTaxonToTaxonClassification(response));

                    if (response.Alternatives != null)
                    {
                        response.Alternatives
                            .Where(a => a.CanonicalName.Equals(scientificName) || a.ScientificName.Contains(scientificName))
                            .Select(this.MapGbifTaxonToTaxonClassification)
                            .ToList()
                            .ForEach(a => result.Add(a));
                    }
                }
            }

            return result;
        }

        private ITaxonClassification MapGbifTaxonToTaxonClassification(IGbifTaxon taxon)
        {
            var result = new TaxonClassificationServiceModel
            {
                ScientificName = taxon.ScientificName,
                CanonicalName = taxon.CanonicalName,
                Rank = taxon.Rank.MapTaxonRankStringToTaxonRankType()
            };

            result.Classification.Add(new TaxonRankServiceModel
            {
                Rank = TaxonRankType.Kingdom,
                ScientificName = taxon.Kingdom
            });

            result.Classification.Add(new TaxonRankServiceModel
            {
                Rank = TaxonRankType.Phylum,
                ScientificName = taxon.Phylum
            });

            result.Classification.Add(new TaxonRankServiceModel
            {
                Rank = TaxonRankType.Class,
                ScientificName = taxon.Class
            });

            result.Classification.Add(new TaxonRankServiceModel
            {
                Rank = TaxonRankType.Order,
                ScientificName = taxon.Order
            });

            result.Classification.Add(new TaxonRankServiceModel
            {
                Rank = TaxonRankType.Family,
                ScientificName = taxon.Family
            });

            result.Classification.Add(new TaxonRankServiceModel
            {
                Rank = TaxonRankType.Genus,
                ScientificName = taxon.Genus
            });

            return result;
        }
    }
}
