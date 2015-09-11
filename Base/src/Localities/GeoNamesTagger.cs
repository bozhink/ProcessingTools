namespace ProcessingTools.BaseLibrary.Geo
{
    public class GeoNamesTagger : TaggerBase
    {
        private const string TagName = "geoname";

        public GeoNamesTagger(Config config, string xml)
            : base(config, xml)
        {
        }

        public GeoNamesTagger(IBase baseObject)
            : base(baseObject)
        {
        }

        public void Tag(IXPathProvider xpathProvider, IDataProvider dataProvider)
        {
            string query = @"select [Name] as [name] from [dbo].[geonames] order by len([Name]) desc;";

            dataProvider.Xml = this.Xml;
            dataProvider.ExecuteSimpleReplaceUsingDatabase(xpathProvider.SelectContentNodesXPath, query, TagName);
            this.Xml = dataProvider.Xml;
        }
    }
}
