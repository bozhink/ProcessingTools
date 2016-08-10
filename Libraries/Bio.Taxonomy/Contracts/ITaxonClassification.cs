namespace ProcessingTools.Bio.Taxonomy.Contracts
{
    public interface ITaxonClassification : IExtendedTaxonRank
    {
        string Superkingdom { get; }

        string Kingdom { get; }

        string Subkingdom { get; }

        string Infrakingdom { get; }

        string Division { get; }

        string Subdivision { get; }

        string Infradivision { get; }

        string Superphylum { get; }

        string Phylum { get; }

        string Subphylum { get; }

        string Infraphylum { get; }

        string Superclass { get; }

        string Class { get; }

        string Subclass { get; }

        string Infraclass { get; }

        string Superorder { get; }

        string Order { get; }

        string Suborder { get; }

        string Infraorder { get; }

        string Parvorder { get; }

        string Superfamily { get; }

        string Family { get; }

        string Subfamily { get; }

        string Supertribe { get; }

        string Tribe { get; }

        string Subtribe { get; }

        string Clade { get; }

        string Subclade { get; }

        string Cohort { get; }

        string Supergroup { get; }

        string Genus { get; }

        string Subgenus { get; }

        string Section { get; }

        string Subsection { get; }

        string Series { get; }

        string Subseries { get; }

        string Superspecies { get; }

        string Species { get; }

        string Subspecies { get; }

        string Variety { get; }

        string Subvariety { get; }

        string Form { get; }

        string Subform { get; }

        string Aberration { get; }

        string Stage { get; }

        string Race { get; }
    }
}