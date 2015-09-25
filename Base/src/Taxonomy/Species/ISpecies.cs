namespace ProcessingTools.BaseLibrary.Taxonomy
{
    public interface ISpecies
    {
        string Genus { get; set; }

        string Subgenus { get; set; }

        string Section { get; set; }

        string Subsection { get; set; }

        string Series { get; set; }

        string Subseries { get; set; }

        string Tribe { get; set; }

        string Superspecies { get; set; }

        string Species { get; set; }

        string Subspecies { get; set; }

        string Variety { get; set; }

        string Subvariety { get; set; }

        string Form { get; set; }

        string Subform { get; set; }

        string Aberration { get; set; }

        string Stage { get; set; }

        string Rank { get; set; }
    }
}
