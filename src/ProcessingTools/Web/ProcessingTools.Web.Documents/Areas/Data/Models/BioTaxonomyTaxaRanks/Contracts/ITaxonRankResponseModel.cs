namespace ProcessingTools.Web.Documents.Areas.Data.Models.BioTaxonomyTaxaRanks.Contracts
{
    public interface ITaxonRankResponseModel
    {
        string Rank { get; }

        string TaxonName { get; }
    }
}
