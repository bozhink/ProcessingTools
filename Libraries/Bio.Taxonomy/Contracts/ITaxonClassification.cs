namespace ProcessingTools.Bio.Taxonomy.Contracts
{
    public interface ITaxonClassification : IExtendedTaxonRank
    {
        string Superkingdom { get; set; }

        string Kingdom { get; set; }

        string Subkingdom { get; set; }

        string Infrakingdom { get; set; }

        string Division { get; set; }

        string Subdivision { get; set; }

        string Infradivision { get; set; }

        string Superphylum { get; set; }

        string Phylum { get; set; }

        string Subphylum { get; set; }

        string Infraphylum { get; set; }

        string Superclass { get; set; }

        string Class { get; set; }

        string Subclass { get; set; }

        string Infraclass { get; set; }

        string Superorder { get; set; }

        string Order { get; set; }

        string Suborder { get; set; }

        string Infraorder { get; set; }

        string Parvorder { get; set; }

        string Superfamily { get; set; }

        string Family { get; set; }

        string Subfamily { get; set; }

        string Supertribe { get; set; }

        string Tribe { get; set; }

        string Subtribe { get; set; }

        string Clade { get; set; }

        string Subclade { get; set; }

        string Cohort { get; set; }

        string Supergroup { get; set; }

        string Genus { get; set; }

        string Subgenus { get; set; }

        string Section { get; set; }

        string Subsection { get; set; }

        string Series { get; set; }

        string Subseries { get; set; }

        string Superspecies { get; set; }

        string Species { get; set; }

        string Subspecies { get; set; }

        string Variety { get; set; }

        string Subvariety { get; set; }

        string Form { get; set; }

        string Subform { get; set; }

        string Aberration { get; set; }

        string Stage { get; set; }

        string Race { get; set; }
    }
}