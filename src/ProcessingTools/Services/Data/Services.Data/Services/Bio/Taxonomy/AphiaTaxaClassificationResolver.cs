namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Clients.Contracts.Bio.Taxonomy;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Abstractions.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    public class AphiaTaxaClassificationResolver : AbstractTaxaInformationResolver<ITaxonClassification>, IAphiaTaxaClassificationResolver
    {
        private readonly IAphiaTaxonClassificationRequester requester;

        public AphiaTaxaClassificationResolver(IAphiaTaxonClassificationRequester requester)
        {
            this.requester = requester ?? throw new ArgumentNullException(nameof(requester));
        }

        protected override async Task<ITaxonClassification[]> ResolveScientificNameAsync(string scientificName)
        {
            var result = await this.requester.ResolveScientificNameAsync(scientificName).ConfigureAwait(false);

            return result;
        }
    }
}
