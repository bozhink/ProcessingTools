namespace ProcessingTools.Nlm.Publishing.Types
{
    using ProcessingTools.Common.Attributes;

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
        [Value("aoi")]
        Aoi,

        /// <summary>
        /// Digital Object Identifier
        /// </summary>
        [Value("doi")]
        Doi,

        /// <summary>
        /// Enzyme nomenclature
        /// </summary>
        /// <see cref="http://www.chem.qmw.ac.uk/iubmb/enzyme/"/>
        [Value("ec")]
        Ec,

        /// <summary>
        /// File Transfer Protocol
        /// </summary>
        [Value("ftp")]
        Ftp,

        /// <summary>
        /// GenBank identifier
        /// </summary>
        [Value("gen")]
        Gen,

        /// <summary>
        /// Translated protein-encoding sequence database
        /// </summary>
        [Value("genpept")]
        Genpept,

        /// <summary>
        /// HighWire Press intrajournal
        /// </summary>
        [Value("highwire")]
        Highwire,

        /// <summary>
        /// NLM title abbreviation
        /// </summary>
        [Value("nlm-ta")]
        NlmTa,

        /// <summary>
        /// Protein Data Bank
        /// </summary>
        /// <see cref="http://www.rcsb.org/pdb/"/>
        [Value("pdb")]
        Pdb,

        /// <summary>
        /// Plant Gene Register
        /// </summary>
        /// <see cref="http://www.tarweed.com/pgr/b"/>
        [Value("pgr")]
        Pgr,

        /// <summary>
        /// Protein Information Resource
        /// </summary>
        /// <see cref="http://pir.georgetown.edu"/>
        [Value("pir")]
        Pir,

        /// <summary>
        /// Protein information resource
        /// </summary>
        /// <see cref="http://pir.georgetown.edu"/>
        [Value("pirdb")]
        Pirdb,

        /// <summary>
        /// PubMed Central identifier
        /// </summary>
        [Value("pmcid")]
        Pmcid,

        /// <summary>
        /// PubMed identifier
        /// </summary>
        [Value("pmid")]
        Pmid,

        /// <summary>
        /// Swiss-Prot
        /// </summary>
        /// <see cref="http://www.ebi.ac.uk/swissprot/"/>
        [Value("sprot")]
        Sprot,

        /// <summary>
        /// Website or web service
        /// </summary>
        [Value("uri")]
        Uri
    }
}
