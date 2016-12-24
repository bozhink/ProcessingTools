namespace ProcessingTools.Web.Documents.Areas.Data.Models.BioTaxonomyTaxaRanks
{
    using System;
    using Contracts;

    public class SearchResposeModel
    {
        private readonly ITaxonRankResponseModel[] taxa;

        public SearchResposeModel(params ITaxonRankResponseModel[] taxa)
        {
            if (taxa == null || taxa.Length < 1)
            {
                throw new ArgumentNullException(nameof(taxa));
            }

            this.taxa = taxa;
        }

        public ITaxonRankResponseModel[] Taxa => this.taxa;
    }
}
