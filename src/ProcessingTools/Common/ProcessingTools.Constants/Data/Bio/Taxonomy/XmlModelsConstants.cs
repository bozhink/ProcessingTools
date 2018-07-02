// <copyright file="XmlModelsConstants.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Constants.Data.Bio.Taxonomy
{
    /// <summary>
    /// Xml models constants
    /// </summary>
    public static class XmlModelsConstants
    {
        /// <summary>
        /// RankListXmlRootNodeName
        /// </summary>
        public const string RankListXmlRootNodeName = "taxa";

        /// <summary>
        /// RankListTaxonXmlModelElementName
        /// </summary>
        public const string RankListTaxonXmlModelElementName = "taxon";

        /// <summary>
        /// RankListIsWhiteListedXmlAttributeName
        /// </summary>
        public const string RankListIsWhiteListedXmlAttributeName = "white-listed";

        /// <summary>
        /// RankListTaxonXmlPartElementName
        /// </summary>
        public const string RankListTaxonXmlPartElementName = "part";

        /// <summary>
        /// RankListTaxonXmlPartValueElementName
        /// </summary>
        public const string RankListTaxonXmlPartValueElementName = "value";

        /// <summary>
        /// RankListTaxonXmlPartRankElementName
        /// </summary>
        public const string RankListTaxonXmlPartRankElementName = "rank";

        /// <summary>
        /// RankListTaxonXmlPartRankValueElementName
        /// </summary>
        public const string RankListTaxonXmlPartRankValueElementName = "value";

        /// <summary>
        /// BlackListXmlRootNodeName
        /// </summary>
        public const string BlackListXmlRootNodeName = "list";

        /// <summary>
        /// BlackListXmlItemElementName
        /// </summary>
        public const string BlackListXmlItemElementName = "item";
    }
}
