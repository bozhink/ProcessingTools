namespace ProcessingTools.Constants.Schema
{
    public sealed class XPathConstants
    {
        public const string SelectContentNodesXPath = ".//p|.//title|.//license-p|.//li|.//th|.//td|.//mixed-citation|.//element-citation|.//nlm-citation|.//tp:nomenclature-citation";

        public const string SelectSpecimenCodesContentNodesXPath = "//p|//li|//th|//td|//title|//tp:nomenclature-citation";

        public const string ArticleZooBankSelfUriXPath = ".//article-meta/self-uri[@content-type='zoobank']";
        public const string ContributorZooBankUriXPath = ".//article-meta/contrib-group/contrib/uri[@content-type='zoobank']";

        public const string NomenclatureXPath = ".//tp:taxon-treatment/tp:nomenclature";

        public const string ZooBankObjectIdXPath = ".//object-id[@content-type='zoobank']";
        public const string IpniObjectIdXPath = ".//object-id[@content-type='ipni']";
    }
}