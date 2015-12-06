namespace ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif.Contracts
{
    public interface IGbifTaxon
    {
        string CanonicalName { get; set; }

        string Class { get; set; }

        int ClassKey { get; set; }

        int Confidence { get; set; }

        string Family { get; set; }

        int FamilyKey { get; set; }

        string Genus { get; set; }

        int GenusKey { get; set; }

        string Kingdom { get; set; }

        int KingdomKey { get; set; }

        string MatchType { get; set; }

        string Note { get; set; }

        string Order { get; set; }

        int OrderKey { get; set; }

        string Phylum { get; set; }

        int PhylumKey { get; set; }

        string Rank { get; set; }

        string ScientificName { get; set; }

        bool Synonym { get; set; }

        int UsageKey { get; set; }
    }
}