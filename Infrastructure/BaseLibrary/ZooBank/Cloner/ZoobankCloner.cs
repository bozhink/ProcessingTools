namespace ProcessingTools.BaseLibrary.ZooBank
{
    using ProcessingTools.Contracts;

    public abstract class ZoobankCloner : Base, ICloner
    {
        protected const string ArticleZooBankSelfUriXPath = "/article/front/article-meta/self-uri[@content-type='zoobank']";
        protected const string ContributorZooBankUriXPath = "/article/front/article-meta/contrib-group/contrib/uri[@content-type='zoobank']";
        protected const string NomenclatureXPath = "//tp:taxon-treatment/tp:nomenclature";
        protected const string ZooBankObjectIdXPath = ".//object-id[@content-type='zoobank']";

        ////protected const string ZooBankObjectIdXPath = ".//object-id[@content-type='ipni']";
        protected const string ZooBankPrefix = "http://zoobank.org/";

        public ZoobankCloner(string xmlContent)
            : base(xmlContent)
        {
        }

        public abstract void Clone();
    }
}