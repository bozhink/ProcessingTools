using System;

namespace ProcessingTools.Base
{
    public class ProductsTagger : Base
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

        public void TagProducts(IXPathProvider xpathProvider, IDataProvider dataProvider)
        {
            string query = @"select [Name] from [dbo].[products] order by len([Name]) desc;";

            dataProvider.Xml = this.Xml;
            dataProvider.ExecuteSimpleReplaceUsingDatabase(xpathProvider.SelectContentNodesXPath, query, TagName);
            this.Xml = dataProvider.Xml;
        }
    }
}
