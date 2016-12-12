namespace ProcessingTools.Bio.Taxonomy.Data.Xml
{
    using Contracts;

    public class XmlTaxaContextProvider : IXmlTaxaContextProvider
    {
        public IXmlTaxaContext Create()
        {
            return new XmlTaxaContext();
        }
    }
}