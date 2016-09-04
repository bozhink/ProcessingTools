namespace ProcessingTools.Web.Documents.Areas.BioTaxonomyData.Models.TaxaRanks.Contracts
{
    public interface ITaxonRankResponseModel
    {
        string Rank { get; }

        string TaxonName { get; }
    }
}
