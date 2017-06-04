namespace ProcessingTools.Enumerations.Nlm
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Describes the type of external link — for example, the specific source to which the link points
    /// (“pdb”, “pir”), the identifier type (“doi”,“aoi”), or the access method (“ftp”, “uri”)
    /// </summary>
    /// <see cref="http://dtd.nlm.nih.gov/publishing/tag-library/3.0/n-7vr0.html"/>
    public enum ExternalLinkType
    {
        /// <summary>
        /// Astronomical Object Identifier
        /// </summary>
        [Display(Name = "aoi")]
        Aoi,

        /// <summary>
        /// Digital Object Identifier
        /// </summary>
        [Display(Name = "doi")]
        Doi,

        /// <summary>
        /// Enzyme nomenclature
        /// </summary>
        /// <see cref="http://www.chem.qmw.ac.uk/iubmb/enzyme/"/>
        [Display(Name = "ec")]
        Ec,

        /// <summary>
        /// File Transfer Protocol
        /// </summary>
        [Display(Name = "ftp")]
        Ftp,

        /// <summary>
        /// GenBank identifier
        /// </summary>
        [Display(Name = "gen")]
        Gen,

        /// <summary>
        /// Translated protein-encoding sequence database
        /// </summary>
        [Display(Name = "genpept")]
        Genpept,

        /// <summary>
        /// HighWire Press intrajournal
        /// </summary>
        [Display(Name = "highwire")]
        Highwire,

        /// <summary>
        /// NLM title abbreviation
        /// </summary>
        [Display(Name = "nlm-ta")]
        NlmTa,

        /// <summary>
        /// Protein Data Bank
        /// </summary>
        /// <see cref="http://www.rcsb.org/pdb/"/>
        [Display(Name = "pdb")]
        Pdb,

        /// <summary>
        /// Plant Gene Register
        /// </summary>
        /// <see cref="http://www.tarweed.com/pgr/b"/>
        [Display(Name = "pgr")]
        Pgr,

        /// <summary>
        /// Protein Information Resource
        /// </summary>
        /// <see cref="http://pir.georgetown.edu"/>
        [Display(Name = "pir")]
        Pir,

        /// <summary>
        /// Protein information resource
        /// </summary>
        /// <see cref="http://pir.georgetown.edu"/>
        [Display(Name = "pirdb")]
        Pirdb,

        /// <summary>
        /// PubMed Central identifier
        /// </summary>
        [Display(Name = "pmcid")]
        Pmcid,

        /// <summary>
        /// PubMed identifier
        /// </summary>
        [Display(Name = "pmid")]
        Pmid,

        /// <summary>
        /// Swiss-Prot
        /// </summary>
        /// <see cref="http://www.ebi.ac.uk/swissprot/"/>
        [Display(Name = "sprot")]
        Sprot,

        /// <summary>
        /// Website or web service
        /// </summary>
        [Display(Name = "uri")]
        Uri
    }
}
