namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts;
    using Contracts.Controllers;
    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;

    [Description("Remove all taxon-name-part tags.")]
    public class RemoveAllTaxonNamePartTagsController : IRemoveAllTaxonNamePartTagsController
    {
        public Task<object> Run(IDocument document, IProgramSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return Task.Run(() => this.RunSync(document));
        }

        // TODO: implement with XSLT
        private object RunSync(IDocument document)
        {
            foreach (XmlNode node in document.SelectNodes("//tn[name(..)!='tp:nomenclature']|//tp:taxon-name[name(..)!='tp:nomenclature']"))
            {
                node.InnerXml = this.RemoveTaxonNamePartTags(node.InnerXml);
            }

            return true;
        }

        private string RemoveTaxonNamePartTags(string content)
        {
            string result = Regex.Replace(content, @"(?<=full-name=""([^<>""]+)""[^>]*>)[^<>]*(?=</)", "$1");
            return Regex.Replace(result, "</?tn-part[^>]*>|</?tp:taxon-name-part[^>]*>", string.Empty);
        }
    }
}
