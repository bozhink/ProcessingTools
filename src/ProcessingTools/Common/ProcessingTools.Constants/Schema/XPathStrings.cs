// <copyright file="XPathStrings.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
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
        /// Article ID of internal (publisher's) type.
        /// </summary>
        public const string ArticleIdOfInternalType = ".//front/article-meta/article-id[@pub-id-type='publisher-id']";

        /// <summary>
        /// Article journal meta ISSN epub.
        /// </summary>
        public const string ArticleJournalMetaIssnEPub = ".//front/journal-meta/issn[@pub-type='epub']";

        /// <summary>
        /// Article journal meta ISSN ppub.
        /// </summary>
        public const string ArticleJournalMetaIssnPPub = ".//front/journal-meta/issn[@pub-type='ppub']";

        /// <summary>
        /// Article journal meta journal abbreviated title.
        /// </summary>
        public const string ArticleJournalMetaJournalAbbreviatedTitle = ".//front/journal-meta/journal-title-group/abbrev-journal-title";

        /// <summary>
        /// Article journal meta journal id.
        /// </summary>
        public const string ArticleJournalMetaJournalId = ".//front/journal-meta/journal-id[@journal-id-type='publisher-id']";

        /// <summary>
        /// Article journal meta journal title.
        /// </summary>
        public const string ArticleJournalMetaJournalTitle = ".//front/journal-meta/journal-title-group/journal-title";

        /// <summary>
        /// Article journal meta publisher name.
        /// </summary>
        public const string ArticleJournalMetaPublisherName = ".//front/journal-meta/publisher/publisher-name";

        /// <summary>
        /// Article meta title.
        /// </summary>
        public const string ArticleMetaTitle = ".//front/article-meta/title-group/article-title";

        /// <summary>
        /// Article meta subtitle.
        /// </summary>
        public const string ArticleMetaSubTitle = ".//front/article-meta/title-group/subtitle";

        /// <summary>
        /// Article meta archival date.
        /// </summary>
        public const string ArticleMetaArchivalDate = ".//front/article-meta/pub-date[@pub-type='archival-date']";

        /// <summary>
        /// Article meta published on.
        /// </summary>
        public const string ArticleMetaPublishedOn = ".//front/article-meta/pub-date[@pub-type='epub']|.//front/article-meta/history/date[@date-type='pub']";

        /// <summary>
        /// Article meta accepted on.
        /// </summary>
        public const string ArticleMetaAcceptedOn = ".//front/article-meta/history/date[@date-type='accepted']";

        /// <summary>
        /// Article meta received on.
        /// </summary>
        public const string ArticleMetaReceivedOn = ".//front/article-meta/history/date[@date-type='received']";

        /// <summary>
        /// Article meta volume series.
        /// </summary>
        public const string ArticleMetaVolumeSeries = ".//front/article-meta/volume-series";

        /// <summary>
        /// Article meta volume.
        /// </summary>
        public const string ArticleMetaVolume = ".//front/article-meta/volume";

        /// <summary>
        /// Article meta issue.
        /// </summary>
        public const string ArticleMetaIssue = ".//front/article-meta/issue";

        /// <summary>
        /// Article meta issue part.
        /// </summary>
        public const string ArticleMetaIssuePart = ".//front/article-meta/issue-part";

        /// <summary>
        /// Article meta e-location ID.
        /// </summary>
        public const string ArticleMetaElocationId = ".//front/article-meta/elocation-id";

        /// <summary>
        /// Article meta first page.
        /// </summary>
        public const string ArticleMetaFirstPage = ".//front/article-meta/fpage";

        /// <summary>
        /// Article meta last page.
        /// </summary>
        public const string ArticleMetaLastPage = ".//front/article-meta/lpage";

        /// <summary>
        /// Article meta counts number of pages.
        /// </summary>
        public const string ArticleMetaCountsNumberOfPages = ".//front/article-meta/counts/page-count/@count";

        /// <summary>
        /// Article meta counts number of references.
        /// </summary>
        public const string ArticleMetaCountsNumberOfReferences = ".//front/article-meta/counts/ref-count/@count";

        /// <summary>
        /// Article ZooBank self-uri.
        /// </summary>
        public const string ArticleZooBankSelfUri = ".//article-meta/self-uri[@content-type='zoobank']";

        /// <summary>
        /// Content nodes.
        /// </summary>
        public const string ContentNodes = ".//p|.//title|.//license-p|.//li|.//th|.//td|.//mixed-citation|.//element-citation|.//nlm-citation|.//tp:nomenclature-citation";

        /// <summary>
        /// Contributor ZooBank uri.
        /// </summary>
        public const string ContributorZooBankUri = ".//article-meta/contrib-group/contrib/uri[@content-type='zoobank']";

        /// <summary>
        /// Coordinate of type latitude with empty latitude and longitude attributes.
        /// </summary>
        public const string CoordinateOfTypeLatitudeWithEmptyLatitudeAndLongitudeAttributes = ".//locality-coordinates[@type='latitude'][normalize-space(@latitude)!='' and normalize-space(@longitude)='']";

        /// <summary>
        /// Coordinate of type longitude with empty latitude and longitude attributes.
        /// </summary>
        public const string CoordinateOfTypeLongitudeWithEmptyLatitudeAndLongitudeAttributes = ".//locality-coordinates[@type='longitude'][normalize-space(@latitude)='' and normalize-space(@longitude)!='']";

        /// <summary>
        /// Coordinate with empty latitude or longitude.
        /// </summary>
        public const string CoordinateWithEmptyLatitudeOrLongitude = ".//locality-coordinates[normalize-space(@latitude)='' or normalize-space(@longitude)='']";

        /// <summary>
        /// Element with full name attribute.
        /// </summary>
        public const string ElementWithFullNameAttribute = ".//*[normalize-space(@" + AttributeNames.FullName + ")!='']";

        /// <summary>
        /// Higher document structure.
        /// </summary>
        public const string HigherDocumentStructure = ".//article[not(ancestor::article)][not(ancestor::document)]|.//document[not(ancestor::article)][not(ancestor::document)]";

        /// <summary>
        /// ID attributes.
        /// </summary>
        public const string IdAttributes = ".//@id";

        /// <summary>
        /// Lower taxon name part with no full name attribute.
        /// </summary>
        public const string LowerTaxonNamePartWithNoFullNameAttribute = LowerTaxonNames + "/tn-part[not(@full-name)]" + TaxonNamePartOfNonAuxiliaryType;

        /// <summary>
        /// Lower taxon names.
        /// </summary>
        public const string LowerTaxonNames = ".//tn[@type='lower']";

        /// <summary>
        /// Lower taxon name with no genus taxon name part.
        /// </summary>
        public const string LowerTaxonNameWithNoGenusTaxonNamePart = LowerTaxonNames + "[count(.//tn-part[@type='genus']) = 0]";

        /// <summary>
        /// Lower taxon name with no taxon name part.
        /// </summary>
        public const string LowerTaxonNameWithNoTaxonNamePart = LowerTaxonNames + "[not(" + ElementNames.TaxonNamePart + ")]";

        /// <summary>
        /// Media element.
        /// </summary>
        public const string MediaElement = ".//media";

        /// <summary>
        /// ObjectId of type IPNI.
        /// </summary>
        public const string ObjectIdOfTypeIpni = ".//object-id[@content-type='ipni']";

        /// <summary>
        /// ObjectId of type ZooBank.
        /// </summary>
        public const string ObjectIdOfTypeZooBank = ".//object-id[@content-type='zoobank']";

        /// <summary>
        /// References XPath.
        /// </summary>
        public const string ReferencesXPath = ".//ref|.//reference";

        /// <summary>
        /// Rid attributes.
        /// </summary>
        public const string RidAttributes = ".//@rid";

        /// <summary>
        /// Root nodes of context.
        /// </summary>
        public const string RootNodesOfContext = "./*";

        /// <summary>
        /// Specimen codes content nodes.
        /// </summary>
        public const string SpecimenCodesContentNodes = "//p|//li|//th|//td|//title|//tp:nomenclature-citation";

        /// <summary>
        /// Table row with coordinate parts Which can be merged.
        /// </summary>
        public const string TableRowWithCoordinatePartsWhichCanBeMerged = ".//tr[count(" + CoordinateOfTypeLatitudeWithEmptyLatitudeAndLongitudeAttributes + ")=1][count(" + CoordinateOfTypeLongitudeWithEmptyLatitudeAndLongitudeAttributes + ")=1]";

        /// <summary>
        /// Taxon name part of non auxiliary type.
        /// </summary>
        public const string TaxonNamePartOfNonAuxiliaryType = "[@type!='sensu'][@type!='hybrid-sign'][@type!='uncertainty-rank'][@type!='infraspecific-rank'][@type!='authority'][@type!='basionym-authority']";

        /// <summary>
        /// Taxon name part of type genus.
        /// </summary>
        public const string TaxonNamePartOfTypeGenus = "tn-part[@type='genus']";

        /// <summary>
        /// Taxon name part of type species.
        /// </summary>
        public const string TaxonNamePartOfTypeSpecies = "tn-part[@type='species']";

        /// <summary>
        /// Taxon name parts of lower taxon names.
        /// </summary>
        public const string TaxonNamePartsOfLowerTaxonNames = LowerTaxonNames + "/" + ElementNames.TaxonNamePart;

        /// <summary>
        /// Taxon treatment nomenclature.
        /// </summary>
        public const string TaxonTreatmentNomenclature = ".//tp:taxon-treatment/tp:nomenclature";

        /// <summary>
        /// XLink href.
        /// </summary>
        public const string XLinkHref = "//graphic/@xlink:href|//inline-graphic/@xlink:href|//media/@xlink:href";
    }
}
