namespace ProcessingTools.Base.Taxonomy
{
    using System.Linq;
    using System.Xml;

    public class TaxonomicNamesValidator : Base, IValidator
    {
        public TaxonomicNamesValidator(string xml)
            : base(xml)
        {
        }

        public TaxonomicNamesValidator(Config config, string xml)
            : base(config, xml)
        {
        }

        public TaxonomicNamesValidator(IBase baseObject)
            : base(baseObject)
        {
        }

        public void Validate()
        {
            string[] scientificNames = this.XmlDocument.ExtractTaxa(true).ToArray<string>();
            XmlDocument gnrXmlResponse = Net.SearchWithGlobalNamesResolver(scientificNames);

            // TODO
            gnrXmlResponse.Save(@"C:\temp\gnr-response.xml");
        }
    }
}