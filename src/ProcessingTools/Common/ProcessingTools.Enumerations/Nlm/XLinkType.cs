// <copyright file="XLinkType.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Enumerations.Nlm
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    /// <summary>
    /// XLink type
    /// See https://dtd.nlm.nih.gov/publishing/tag-library/3.0/index.html
    /// </summary>
    public enum XLinkType
    {
        /// <summary>
        /// Simple (one-ended)
        /// </summary>
        [Display(Name = "simple")]
        [XmlEnum(Name = "simple")]
        Simple,

        /// <summary>
        /// Locator
        /// </summary>
        [Display(Name = "locator")]
        [XmlEnum(Name = "locator")]
        Locator,

        /// <summary>
        /// Title
        /// </summary>
        [Display(Name = "title")]
        [XmlEnum(Name = "title")]
        Title,

        /// <summary>
        /// Extended (multi-way)
        /// </summary>
        [Display(Name = "extended")]
        [XmlEnum(Name = "extended")]
        Extended
    }
}
