// <copyright file="XPathStrings.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Constants.Schema
{
    /// <summary>
    /// XPath strings.
    /// </summary>
    public static class XPathStrings
    {
        /// <summary>
        /// Article ID of type DOI.
        /// </summary>
        public const string ArticleIdOfTypeDoi = ".//front/article-meta/article-id[@pub-id-type='doi']";

        /// <summary>
        /// Article journal meta ISSN epub.
        /// </summary>
        public const string ArticleJournalMetaIssnEPub = ".//front/journal-meta/issn[@pub-type='epub']";

        /// <summary>
        /// Article journal meta ISSN ppub.
        /// </summary>
        public const string ArticleJournalMetaIssnPPub = ".//front/journal-meta/issn[@pub-type='ppub']";

        /// <summary>
        /// ArticleJournalMetaJournalAbbreviatedTitle
        /// </summary>
        public const string ArticleJournalMetaJournalAbbreviatedTitle = ".//front/journal-meta/journal-title-group/abbrev-journal-title";

        /// <summary>
        /// ArticleJournalMetaJournalId
        /// </summary>
        public const string ArticleJournalMetaJournalId = ".//front/journal-meta/journal-id[@journal-id-type='publisher-id']";

        /// <summary>
        /// ArticleJournalMetaJournalTitle
        /// </summary>
        public const string ArticleJournalMetaJournalTitle = ".//front/journal-meta/journal-title-group/journal-title";

        /// <summary>
        /// ArticleJournalMetaPublisherName
        /// </summary>
        public const string ArticleJournalMetaPublisherName = ".//front/journal-meta/publisher/publisher-name";

        /// <summary>
        /// ArticleMetaElocationId
        /// </summary>
        public const string ArticleMetaElocationId = ".//front/article-meta/elocation-id";

        /// <summary>
        /// ArticleMetaFirstPage
        /// </summary>
        public const string ArticleMetaFirstPage = ".//front/article-meta/fpage";

        /// <summary>
        /// ArticleMetaIssue
        /// </summary>
        public const string ArticleMetaIssue = ".//front/article-meta/issue";

        /// <summary>
        /// ArticleMetaLastPage
        /// </summary>
        public const string ArticleMetaLastPage = ".//front/article-meta/lpage";

        /// <summary>
        /// ArticleMetaVolume
        /// </summary>
        public const string ArticleMetaVolume = ".//front/article-meta/volume";

        /// <summary>
        /// ArticleZooBankSelfUri
        /// </summary>
        public const string ArticleZooBankSelfUri = ".//article-meta/self-uri[@content-type='zoobank']";

        /// <summary>
        /// ContentNodes
        /// </summary>
        public const string ContentNodes = ".//p|.//title|.//license-p|.//li|.//th|.//td|.//mixed-citation|.//element-citation|.//nlm-citation|.//tp:nomenclature-citation";

        /// <summary>
        /// ContributorZooBankUri
        /// </summary>
        public const string ContributorZooBankUri = ".//article-meta/contrib-group/contrib/uri[@content-type='zoobank']";

        /// <summary>
        /// CoordinateOfTypeLatitudeWithEmptyLatitudeAndLongitudeAttributes
        /// </summary>
        public const string CoordinateOfTypeLatitudeWithEmptyLatitudeAndLongitudeAttributes = ".//locality-coordinates[@type='latitude'][normalize-space(@latitude)!='' and normalize-space(@longitude)='']";

        /// <summary>
        /// CoordinateOfTypeLongitudeWithEmptyLatitudeAndLongitudeAttributes
        /// </summary>
        public const string CoordinateOfTypeLongitudeWithEmptyLatitudeAndLongitudeAttributes = ".//locality-coordinates[@type='longitude'][normalize-space(@latitude)='' and normalize-space(@longitude)!='']";

        /// <summary>
        /// CoordinateWithEmptyLatitudeOrLongitude
        /// </summary>
        public const string CoordinateWithEmptyLatitudeOrLongitude = ".//locality-coordinates[normalize-space(@latitude)='' or normalize-space(@longitude)='']";

        /// <summary>
        /// ElementWithFullNameAttribute
        /// </summary>
        public const string ElementWithFullNameAttribute = ".//*[normalize-space(@" + AttributeNames.FullName + ")!='']";

        /// <summary>
        /// HigherDocumentStructure
        /// </summary>
        public const string HigherDocumentStructure = ".//article[not(ancestor::article)][not(ancestor::document)]|.//document[not(ancestor::article)][not(ancestor::document)]";

        /// <summary>
        /// IdAttributes
        /// </summary>
        public const string IdAttributes = ".//@id";

        /// <summary>
        /// LowerTaxonNamePartWithNoFullNameAttribute
        /// </summary>
        public const string LowerTaxonNamePartWithNoFullNameAttribute = LowerTaxonNames + "/tn-part[not(@full-name)]" + TaxonNamePartOfNonAuxiliaryType;

        /// <summary>
        /// LowerTaxonNames
        /// </summary>
        public const string LowerTaxonNames = ".//tn[@type='lower']";

        /// <summary>
        /// LowerTaxonNameWithNoGenusTaxonNamePart
        /// </summary>
        public const string LowerTaxonNameWithNoGenusTaxonNamePart = LowerTaxonNames + "[count(.//tn-part[@type='genus']) = 0]";

        /// <summary>
        /// LowerTaxonNameWithNoTaxonNamePart
        /// </summary>
        public const string LowerTaxonNameWithNoTaxonNamePart = LowerTaxonNames + "[not(" + ElementNames.TaxonNamePart + ")]";

        /// <summary>
        /// MediaElement
        /// </summary>
        public const string MediaElement = ".//media";

        /// <summary>
        /// ObjectIdOfTypeIpni
        /// </summary>
        public const string ObjectIdOfTypeIpni = ".//object-id[@content-type='ipni']";

        /// <summary>
        /// ObjectIdOfTypeZooBank
        /// </summary>
        public const string ObjectIdOfTypeZooBank = ".//object-id[@content-type='zoobank']";

        /// <summary>
        /// ReferencesXPath
        /// </summary>
        public const string ReferencesXPath = ".//ref|.//reference";

        /// <summary>
        /// RidAttributes
        /// </summary>
        public const string RidAttributes = ".//@rid";

        /// <summary>
        /// RootNodesOfContext
        /// </summary>
        public const string RootNodesOfContext = "./*";

        /// <summary>
        /// SpecimenCodesContentNodes
        /// </summary>
        public const string SpecimenCodesContentNodes = "//p|//li|//th|//td|//title|//tp:nomenclature-citation";

        /// <summary>
        /// TableRowWithCoordinatePartsWichCanBeMerged
        /// </summary>
        public const string TableRowWithCoordinatePartsWichCanBeMerged = ".//tr[count(" + CoordinateOfTypeLatitudeWithEmptyLatitudeAndLongitudeAttributes + ")=1][count(" + CoordinateOfTypeLongitudeWithEmptyLatitudeAndLongitudeAttributes + ")=1]";

        /// <summary>
        /// TaxonNamePartOfNonAuxiliaryType
        /// </summary>
        public const string TaxonNamePartOfNonAuxiliaryType = "[@type!='sensu'][@type!='hybrid-sign'][@type!='uncertainty-rank'][@type!='infraspecific-rank'][@type!='authority'][@type!='basionym-authority']";

        /// <summary>
        /// TaxonNamePartOfTypeGenus
        /// </summary>
        public const string TaxonNamePartOfTypeGenus = "tn-part[@type='genus']";

        /// <summary>
        /// TaxonNamePartOfTypeSpecies
        /// </summary>
        public const string TaxonNamePartOfTypeSpecies = "tn-part[@type='species']";

        /// <summary>
        /// TaxonNamePartsOfLowerTaxonNames
        /// </summary>
        public const string TaxonNamePartsOfLowerTaxonNames = LowerTaxonNames + "/" + ElementNames.TaxonNamePart;

        /// <summary>
        /// TaxonTreatmentNomenclature
        /// </summary>
        public const string TaxonTreatmentNomenclature = ".//tp:taxon-treatment/tp:nomenclature";

        /// <summary>
        /// XLinkHref
        /// </summary>
        public const string XLinkHref = "//graphic/@xlink:href|//inline-graphic/@xlink:href|//media/@xlink:href";
    }
}
