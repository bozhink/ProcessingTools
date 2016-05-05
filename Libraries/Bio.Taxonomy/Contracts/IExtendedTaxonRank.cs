namespace ProcessingTools.Bio.Taxonomy.Contracts
{
    public interface IExtendedTaxonRank : ITaxonRank
    {
        string CanonicalName { get; set; }

        string Authority { get; set; }
    }
}
