namespace ProcessingTools.Bio.Taxonomy.Data.Xml
{
    using Contracts;
    using ProcessingTools.Configurator;

    public class XmlBiotaxonomicBlackListContextProvider : IXmlBiotaxonomicBlackListContextProvider
    {
        public IXmlBiotaxonomicBlackListContext Create()
        {
            return new XmlBiotaxonomicBlackListContext(ConfigBuilder.Create());
        }
    }
}
