namespace ProcessingTools.Tagger.Controllers
{
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;

    [Description("Remove all taxon-name-part tags.")]
    public class RemoveAllTaxonNamePartTagsController : TaggerControllerFactory, IRemoveAllTaxonNamePartTagsController
    {
        public RemoveAllTaxonNamePartTagsController(IDocumentFactory documentFactory)
            : base(documentFactory)
        {
        }

        protected override Task Run(IDocument document, IProgramSettings settings) => Task.Run(() => this.RunSync(document));

        // TODO: implement with XSLT
        private void RunSync(IDocument document)
        {
            foreach (XmlNode node in document.SelectNodes("//tn[name(..)!='tp:nomenclature']|//tp:taxon-name[name(..)!='tp:nomenclature']"))
            {
                node.InnerXml = this.RemoveTaxonNamePartTags(node.InnerXml);
            }
        }

        private string RemoveTaxonNamePartTags(string content)
        {
            string result = Regex.Replace(content, @"(?<=full-name=""([^<>""]+)""[^>]*>)[^<>]*(?=</)", "$1");
            return Regex.Replace(result, "</?tn-part[^>]*>|</?tp:taxon-name-part[^>]*>", string.Empty);
        }
    }
}
