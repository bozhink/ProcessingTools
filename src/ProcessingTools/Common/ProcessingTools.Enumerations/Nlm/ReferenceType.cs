// <copyright file="ReferenceType.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Enumerations.Nlm
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    /// <summary>
    /// Type of Cross Reference
    /// See https://dtd.nlm.nih.gov/publishing/tag-library/3.0/index.html
    /// </summary>
    public enum ReferenceType
    {
        /// <summary>
        /// Affiliation
        /// </summary>
        [Display(Name = "aff")]
        [XmlEnum(Name = "aff")]
        Affiliation,

        /// <summary>
        /// Appendix
        /// </summary>
        [Display(Name = "app")]
        [XmlEnum(Name = "app")]
        Appendix,

        /// <summary>
        /// AuthorNotes
        /// </summary>
        [Display(Name = "author-notes")]
        [XmlEnum(Name = "author-notes")]
        AuthorNotes,

        /// <summary>
        /// Bibliographic reference
        /// </summary>
        [Display(Name = "bibr")]
        [XmlEnum(Name = "bibr")]
        BibliographicReference,

        /// <summary>
        /// Chemical structure
        /// </summary>
        [Display(Name = "chem")]
        [XmlEnum(Name = "chem")]
        ChemicalStructure,

        /// <summary>
        /// Contributor
        /// </summary>
        [Display(Name = "contrib")]
        [XmlEnum(Name = "contrib")]
        Contributor,

        /// <summary>
        /// Corresponding author
        /// </summary>
        [Display(Name = "corresp")]
        [XmlEnum(Name = "corresp")]
        CorrespondingAuthor,

        /// <summary>
        /// Display formula
        /// </summary>
        [Display(Name = "disp-formula")]
        [XmlEnum(Name = "disp-formula")]
        DisplayFormula,

        /// <summary>
        /// Figure
        /// </summary>
        [Display(Name = "fig")]
        [XmlEnum(Name = "fig")]
        Figure,

        /// <summary>
        /// Footnote
        /// </summary>
        [Display(Name = "fn")]
        [XmlEnum(Name = "fn")]
        Footnote,

        /// <summary>
        /// Keyword
        /// </summary>
        [Display(Name = "kwd")]
        [XmlEnum(Name = "kwd")]
        Keyword,

        /// <summary>
        /// List
        /// </summary>
        [Display(Name = "list")]
        [XmlEnum(Name = "list")]
        List,

        /// <summary>
        /// Other
        /// </summary>
        [Display(Name = "other")]
        [XmlEnum(Name = "other")]
        Other,

        /// <summary>
        /// Plate
        /// </summary>
        [Display(Name = "plate")]
        [XmlEnum(Name = "plate")]
        Plate,

        /// <summary>
        /// Scheme
        /// </summary>
        [Display(Name = "scheme")]
        [XmlEnum(Name = "scheme")]
        Scheme,

        /// <summary>
        /// Section
        /// </summary>
        [Display(Name = "sec")]
        [XmlEnum(Name = "sec")]
        Section,

        /// <summary>
        /// Statement
        /// </summary>
        [Display(Name = "statement")]
        [XmlEnum(Name = "statement")]
        Statement,

        /// <summary>
        /// Supplementary material
        /// </summary>
        [Display(Name = "supplementary-material")]
        [XmlEnum(Name = "supplementary-material")]
        SupplementaryMaterial,

        /// <summary>
        /// Table
        /// </summary>
        [Display(Name = "table")]
        [XmlEnum(Name = "table")]
        Table,

        /// <summary>
        /// Table footnote
        /// </summary>
        [Display(Name = "table-fn")]
        [XmlEnum(Name = "table-fn")]
        TableFootnote,

        /// <summary>
        /// Boxed text
        /// </summary>
        [Display(Name = "boxed-text")]
        [XmlEnum(Name = "boxed-text")]
        BoxedText
    }
}
