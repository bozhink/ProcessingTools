namespace ProcessingTools.Models.Contracts.Bio.Taxonomy
{
    public interface IExtendedTaxonRank : ITaxonRank
    {
        string CanonicalName { get; }

        string Authority { get; }
    }
}
