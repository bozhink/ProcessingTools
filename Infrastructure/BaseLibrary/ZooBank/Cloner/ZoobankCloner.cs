namespace ProcessingTools.BaseLibrary.ZooBank
{
    using System.Threading.Tasks;

    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider;

    public abstract class ZoobankCloner : TaxPubDocument, ICloner
    {
        protected const string ArticleZooBankSelfUriXPath = "//article-meta/self-uri[@content-type='zoobank']";
        protected const string ContributorZooBankUriXPath = "//article-meta/contrib-group/contrib/uri[@content-type='zoobank']";
        protected const string NomenclatureXPath = "//tp:taxon-treatment/tp:nomenclature";
        protected const string ZooBankObjectIdXPath = ".//object-id[@content-type='zoobank']";

        ////protected const string ZooBankObjectIdXPath = ".//object-id[@content-type='ipni']";
        protected const string ZooBankPrefix = "http://zoobank.org/";

        public ZoobankCloner(string xmlContent)
            : base(xmlContent)
        {
        }

        public abstract Task Clone();
    }
}