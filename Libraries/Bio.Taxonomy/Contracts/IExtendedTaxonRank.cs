namespace ProcessingTools.Bio.Taxonomy.Contracts
{
    public interface IExtendedTaxonRank : ITaxonRank
    {
        string CanonicalName { get; }

        string Authority { get;  }
    }
}
