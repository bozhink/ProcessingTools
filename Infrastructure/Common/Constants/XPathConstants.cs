namespace ProcessingTools.Common.Constants
{
    public sealed class XPathConstants
    {
        public const string SelectContentNodesXPath = "//p|//license-p|//title|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation";

        public const string SelectSpecimenCodesContentNodesXPath = "//p|//li|//th|//td|//title|//tp:nomenclature-citation";

        public const string SelectContentNodesXPathTemplate = "//p[{0}]|//title[{0}]|//license-p[{0}]|//li[{0}]|//th[{0}]|//td[{0}]|//mixed-citation[{0}]|//element-citation[{0}]|//nlm-citation[{0}]|//tp:nomenclature-citation[{0}]";

        public const string ArticleZooBankSelfUriXPath = ".//article-meta/self-uri[@content-type='zoobank']";
        public const string ContributorZooBankUriXPath = ".//article-meta/contrib-group/contrib/uri[@content-type='zoobank']";

        public const string NomenclatureXPath = ".//tp:taxon-treatment/tp:nomenclature";

        public const string ZooBankObjectIdXPath = ".//object-id[@content-type='zoobank']";
        public const string IpniObjectIdXPath = ".//object-id[@content-type='ipni']";
    }
}