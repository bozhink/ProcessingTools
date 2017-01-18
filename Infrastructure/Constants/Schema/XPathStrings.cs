namespace ProcessingTools.Constants.Schema
{
    public sealed class XPathStrings
    {
        public const string ArticleIdOfTypeDoi = "/article/front/article-meta/article-id[@pub-id-type='doi']";
        public const string ArticleJournalMetaIssnEPub = "/article/front/journal-meta/issn[@pub-type='epub']";
        public const string ArticleJournalMetaIssnPPub = "/article/front/journal-meta/issn[@pub-type='ppub']";
        public const string ArticleJournalMetaJournalAbbreviatedTitle = "/article/front/journal-meta/journal-title-group/abbrev-journal-title";
        public const string ArticleJournalMetaJournalId = "/article/front/journal-meta/journal-id[@journal-id-type='publisher-id']";
        public const string ArticleJournalMetaJournalTitle = "/article/front/journal-meta/journal-title-group/journal-title";
        public const string ArticleJournalMetaPublisherName = "/article/front/journal-meta/publisher/publisher-name";
        public const string ArticleMetaElocationId = "/article/front/article-meta/elocation-id";
        public const string ArticleMetaFirstPage = "/article/front/article-meta/fpage";
        public const string ArticleMetaIssue = "/article/front/article-meta/issue";
        public const string ArticleMetaLastPage = "/article/front/article-meta/lpage";
        public const string ArticleMetaVolume = "/article/front/article-meta/volume";
        public const string ArticleZooBankSelfUri = ".//article-meta/self-uri[@content-type='zoobank']";
        public const string ContentNodes = ".//p|.//title|.//license-p|.//li|.//th|.//td|.//mixed-citation|.//element-citation|.//nlm-citation|.//tp:nomenclature-citation";
        public const string ContributorZooBankUri = ".//article-meta/contrib-group/contrib/uri[@content-type='zoobank']";
        public const string CoordinateOfTypeLatitudeWithEmptyLatitudeAndLongitudeAttributes = ".//locality-coordinates[@type='latitude'][normalize-space(@latitude)!='' and normalize-space(@longitude)='']";
        public const string CoordinateOfTypeLongitudeWithEmptyLatitudeAndLongitudeAttributes = ".//locality-coordinates[@type='longitude'][normalize-space(@latitude)='' and normalize-space(@longitude)!='']";
        public const string CoordinateWithEmptyLatitudeOrLongitude = ".//locality-coordinates[normalize-space(@latitude)='' or normalize-space(@longitude)='']";
        public const string HigherDocumentStructure = "//article[not(ancestor::article)][not(ancestor::document)]|//document[not(ancestor::article)][not(ancestor::document)]";
        public const string IdAttributes = ".//@id";
        public const string LowerTaxonNames = ".//tn[@type='lower']";
        public const string MediaElement = ".//media";
        public const string ObjectIdOfTypeIpni = ".//object-id[@content-type='ipni']";
        public const string ObjectIdOfTypeZooBank = ".//object-id[@content-type='zoobank']";
        public const string RidAttributes = ".//@rid";
        public const string RootNodesOfContext = "./*";
        public const string SpecimenCodesContentNodes = "//p|//li|//th|//td|//title|//tp:nomenclature-citation";
        public const string TableRowWithCoordinatePartsWichCanBeMerged = ".//tr[count(" + CoordinateOfTypeLatitudeWithEmptyLatitudeAndLongitudeAttributes + ")=1][count(" + CoordinateOfTypeLongitudeWithEmptyLatitudeAndLongitudeAttributes + ")=1]";
        public const string TaxonNamePartOfTypeGenus = "tn-part[@type='genus']";
        public const string TaxonNamePartOfTypeSpecies = "tn-part[@type='species']";
        public const string TaxonNamePartsOfLowerTaxonNames = LowerTaxonNames + "/" + ElementNames.TaxonNamePart;
        public const string TaxonTreatmentNomenclature = ".//tp:taxon-treatment/tp:nomenclature";
        public const string XLinkHref = "//graphic/@xlink:href|//inline-graphic/@xlink:href|//media/@xlink:href";
    }
}
