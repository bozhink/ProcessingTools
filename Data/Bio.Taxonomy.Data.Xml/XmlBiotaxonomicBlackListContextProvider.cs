namespace ProcessingTools.Bio.Taxonomy.Data.Xml
{
    using Contracts;

    public class XmlBiotaxonomicBlackListContextProvider : IXmlBiotaxonomicBlackListContextProvider
    {
        public IXmlBiotaxonomicBlackListContext Create()
        {
            return new XmlBiotaxonomicBlackListContext();
        }
    }
}
