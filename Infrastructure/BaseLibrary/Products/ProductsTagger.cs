namespace ProcessingTools.BaseLibrary.Products
{
    using Configurator;
    using Contracts;
    using ProcessingTools.Contracts;

    public class ProductsTagger : TaggerBase
    {
        private const string TagName = "product";

        public ProductsTagger(Config config, string xml)
            : base(config, xml)
        {
        }

        public ProductsTagger(IBase baseObject)
            : base(baseObject)
        {
        }

        public void Tag(IXPathProvider xpathProvider, IDataProvider dataProvider)
        {
            string query = @"select [Name] as [name] from [dbo].[products] order by len([Name]) desc;";

            dataProvider.Xml = this.Xml;
            dataProvider.ExecuteSimpleReplaceUsingDatabase(xpathProvider.SelectContentNodesXPath, query, TagName);
            this.Xml = dataProvider.Xml;
        }
    }
}
