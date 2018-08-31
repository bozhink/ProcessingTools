// <copyright file="ExternalLinkType.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Enumerations.Nlm
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    /// <summary>
    /// Describes the type of external link — for example, the specific source to which the link points
    /// (“pdb”, “pir”), the identifier type (“doi”,“aoi”), or the access method (“ftp”, “uri”)
    /// See http://dtd.nlm.nih.gov/publishing/tag-library/3.0/n-7vr0.html
    /// </summary>
    public enum ExternalLinkType
    {
        /// <summary>
        /// Astronomical Object Identifier
        /// </summary>
        [Display(Name = "aoi")]
        [XmlEnum(Name = "aoi")]
        Aoi,

        /// <summary>
        /// Digital Object Identifier
        /// </summary>
        [Display(Name = "doi")]
        [XmlEnum(Name = "doi")]
        Doi,

        /// <summary>
        /// Enzyme nomenclature
        /// See http://www.chem.qmw.ac.uk/iubmb/enzyme/
        /// </summary>
        [Display(Name = "ec")]
        [XmlEnum(Name = "ec")]
        Ec,

        /// <summary>
        /// File Transfer Protocol
        /// </summary>
        [Display(Name = "ftp")]
        [XmlEnum(Name = "ftp")]
        Ftp,

        /// <summary>
        /// GenBank identifier
        /// </summary>
        [Display(Name = "gen")]
        [XmlEnum(Name = "gen")]
        Gen,

        /// <summary>
        /// Translated protein-encoding sequence database
        /// </summary>
        [Display(Name = "genpept")]
        [XmlEnum(Name = "genpept")]
        Genpept,

        /// <summary>
        /// HighWire Press intrajournal
        /// </summary>
        [Display(Name = "highwire")]
        [XmlEnum(Name = "highwire")]
        Highwire,

        /// <summary>
        /// NLM title abbreviation
        /// </summary>
        [Display(Name = "nlm-ta")]
        [XmlEnum(Name = "nlm-ta")]
        NlmTa,

        /// <summary>
        /// Protein Data Bank
        /// See http://www.rcsb.org/pdb/
        /// </summary>
        [Display(Name = "pdb")]
        [XmlEnum(Name = "pdb")]
        Pdb,

        /// <summary>
        /// Plant Gene Register
        /// See http://www.tarweed.com/pgr/b
        /// </summary>
        [Display(Name = "pgr")]
        [XmlEnum(Name = "pgr")]
        Pgr,

        /// <summary>
        /// Protein Information Resource
        /// See http://pir.georgetown.edu
        /// </summary>
        [Display(Name = "pir")]
        [XmlEnum(Name = "pir")]
        Pir,

        /// <summary>
        /// Protein information resource
        /// See http://pir.georgetown.edu
        /// </summary>
        [Display(Name = "pirdb")]
        [XmlEnum(Name = "pirdb")]
        Pirdb,

        /// <summary>
        /// PubMed Central identifier
        /// </summary>
        [Display(Name = "pmcid")]
        [XmlEnum(Name = "pmcid")]
        Pmcid,

        /// <summary>
        /// PubMed identifier
        /// </summary>
        [Display(Name = "pmid")]
        [XmlEnum(Name = "pmid")]
        Pmid,

        /// <summary>
        /// Swiss-Prot
        /// See http://www.ebi.ac.uk/swissprot/
        /// </summary>
        [Display(Name = "sprot")]
        [XmlEnum(Name = "sprot")]
        Sprot,

        /// <summary>
        /// Website or web service
        /// </summary>
        [Display(Name = "uri")]
        [XmlEnum(Name = "uri")]
        Uri
    }
}
