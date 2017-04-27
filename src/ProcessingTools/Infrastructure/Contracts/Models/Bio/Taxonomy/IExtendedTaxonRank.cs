namespace ProcessingTools.Contracts.Models.Bio.Taxonomy
{
    public interface IExtendedTaxonRank : ITaxonRank
    {
        string CanonicalName { get; }

        string Authority { get; }
    }
}
