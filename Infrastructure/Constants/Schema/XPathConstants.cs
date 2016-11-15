namespace ProcessingTools.Constants.Schema
{
    public sealed class XPathConstants
    {
        public const string ArticleIdOfTypeDoiXPath = "/article/front/article-meta/article-id[@pub-id-type='doi']";
        public const string ArticleMetaElocationIdXPath = "/article/front/article-meta/elocation-id";
        public const string ArticleMetaFirstPageXPath = "/article/front/article-meta/fpage";
        public const string ArticleMetaIssueXPath = "/article/front/article-meta/issue";
        public const string ArticleMetaLastPageXPath = "/article/front/article-meta/lpage";
        public const string ArticleMetaVolumeXPath = "/article/front/article-meta/volume";
        public const string ArticleZooBankSelfUriXPath = ".//article-meta/self-uri[@content-type='zoobank']";
        public const string ContributorZooBankUriXPath = ".//article-meta/contrib-group/contrib/uri[@content-type='zoobank']";
        public const string IpniObjectIdXPath = ".//object-id[@content-type='ipni']";
        public const string NomenclatureXPath = ".//tp:taxon-treatment/tp:nomenclature";
        public const string SelectContentNodesXPath = ".//p|.//title|.//license-p|.//li|.//th|.//td|.//mixed-citation|.//element-citation|.//nlm-citation|.//tp:nomenclature-citation";
        public const string SelectSpecimenCodesContentNodesXPath = "//p|//li|//th|//td|//title|//tp:nomenclature-citation";
        public const string XLinkHrefXPath = "//graphic/@xlink:href|//inline-graphic/@xlink:href|//media/@xlink:href";
        public const string ZooBankObjectIdXPath = ".//object-id[@content-type='zoobank']";
    }
}
