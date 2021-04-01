// <copyright file="XmlModelsConstants.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Constants.Data.Bio.Taxonomy
{
    /// <summary>
    /// XML models constants.
    /// </summary>
    public static class XmlModelsConstants
    {
        /// <summary>
        /// Rank list XML root node name.
        /// </summary>
        public const string RankListXmlRootNodeName = "taxa";

        /// <summary>
        /// Rank list taxon XML model element name.
        /// </summary>
        public const string RankListTaxonXmlModelElementName = "taxon";

        /// <summary>
        /// Rank list is white listed XML attribute name.
        /// </summary>
        public const string RankListIsWhiteListedXmlAttributeName = "white-listed";

        /// <summary>
        /// Rank list taxon XML part element name.
        /// </summary>
        public const string RankListTaxonXmlPartElementName = "part";

        /// <summary>
        /// Rank list taxon XML part value element name.
        /// </summary>
        public const string RankListTaxonXmlPartValueElementName = "value";

        /// <summary>
        /// Rank list taxon XML part rank element name.
        /// </summary>
        public const string RankListTaxonXmlPartRankElementName = "rank";

        /// <summary>
        /// Rank list taxon XML part rank value element name.
        /// </summary>
        public const string RankListTaxonXmlPartRankValueElementName = "value";

        /// <summary>
        /// Black-list XML root node name.
        /// </summary>
        public const string BlackListXmlRootNodeName = "list";

        /// <summary>
        /// Black-list XML item element name.
        /// </summary>
        public const string BlackListXmlItemElementName = "item";
    }
}
