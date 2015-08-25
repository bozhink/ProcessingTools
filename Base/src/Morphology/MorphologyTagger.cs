namespace ProcessingTools.Base
{
    public class MorphologyTagger : TaggerBase
    {
        private const string TagName = "morphology-part";

        public MorphologyTagger(Config config, string xml)
            : base(config, xml)
        {
        }

        public MorphologyTagger(IBase baseObject)
            : base(baseObject)
        {
        }

        public void TagMorphology(IXPathProvider xpathProvider, IDataProvider dataProvider)
        {
            string query = @"select [Name] as [name] from [dbo].[morphology] order by len([Name]) desc;";

            dataProvider.Xml = this.Xml;
            dataProvider.ExecuteSimpleReplaceUsingDatabase(xpathProvider.SelectContentNodesXPath, query, TagName);
            this.Xml = dataProvider.Xml;
        }
    }
}