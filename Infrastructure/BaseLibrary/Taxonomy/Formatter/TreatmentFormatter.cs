namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider;
    using ProcessingTools.Extensions;
    using ProcessingTools.Xml.Extensions;

    public class TreatmentFormatter : TaxPubDocument, IFormatter
    {
        private ILogger logger;

        public TreatmentFormatter(string xml, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
        }

        public Task Format()
        {
            return Task.Run(() =>
            {
                try
                {
                    this.FormatNomenclatureCitations();
                    this.FormatNomenclaturesWithTitle();
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, string.Empty);
                }
            });
        }

        private void FormatNomenclaturesWithTitle()
        {
            const string XPath = "//tp:nomenclature[title]";
            this.XmlDocument.SelectNodes(XPath, this.NamespaceManager)
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(nomenclature =>
                {
                    nomenclature.SelectNodes(".//i[tn]").ReplaceXmlNodeByItsInnerXml();

                    this.FormatNomencatureContent(nomenclature);
                    this.FormatObjectIdInNomenclature(nomenclature);
                });
        }

        private void FormatNomenclatureCitations()
        {
            const string XPath = "//tp:nomenclature-citation[count(comment) = count(node()[normalize-space()!=''])][name(comment/node()[normalize-space()!=''][position()=1])='tn']";
            this.XmlDocument.SelectNodes(XPath, this.NamespaceManager)
                .Cast<XmlNode>()
                .AsParallel()
                .ForAll(citation =>
                {
                    var taxonNode = citation.SelectSingleNode("comment/node()[normalize-space()!=''][position()=1][name()='tn']");

                    if (taxonNode != null)
                    {
                        citation.PrependChild(taxonNode);
                    }
                });
        }

        private void FormatNomencatureContent(XmlNode nomenclature)
        {
            const string TitleNodeName = "title";
            const string LabelNodeName = "label";

            const string TaxonAuthorityStatusNodeName = "AuthorityStatus";

            const string TaxonNodesPrefix = "tp";
            const string TaxonAuthorityNodeName = "taxon-authority";
            const string TaxonStatusNodeName = "taxon-status";

            string namespaceUri = this.NamespaceManager.LookupNamespace(TaxonNodesPrefix);

            XmlNode titleNode = nomenclature.SelectSingleNode(TitleNodeName);
            if (titleNode != null)
            {
                titleNode.InnerXml = titleNode.InnerXml
                    .RegexReplace("</?i>", string.Empty)
                    .RegexReplace(@"\s+", " ")
                    .Trim();

                var matchLabel = new Regex(@"\A([^<>]+?)\s*(?=<tn\b)");
                titleNode.ReplaceXmlNodeContentByRegex(matchLabel, string.Empty, "$1", string.Empty, LabelNodeName);

                var matchAuthority = new Regex(@"(?<=</tn>)\s*([^<>]+)\Z");
                titleNode.ReplaceXmlNodeContentByRegex(matchAuthority, string.Empty, "$1", string.Empty, TaxonAuthorityStatusNodeName);

                titleNode.ReplaceXmlNodeByItsInnerXml();
            }

            XmlNode authorityStatusNode = nomenclature.SelectSingleNode(TaxonAuthorityStatusNodeName);
            if (authorityStatusNode != null)
            {
                authorityStatusNode.InnerXml = authorityStatusNode.InnerXml.Trim();

                var matchWholeContentAsStatus = new Regex(@"(?<=(?:\A|\W\s*))\b([Ii]ncertae\s+[Ss]edis|nom\.?\s+cons\.?|(?:n\.\s*[a-z]+)\b|(?:[a-z]+\.\s*)?spp\b\.?|[a-z]+\.\s*\b(?:n|nov|r|rev)\b\.?|new record)\Z");
                authorityStatusNode.ReplaceXmlNodeContentByRegex(matchWholeContentAsStatus, string.Empty, "$1", string.Empty, TaxonStatusNodeName, TaxonNodesPrefix, namespaceUri);

                var matchWholeContentAsAuthoriry = new Regex(@"\A(?!<)([\w\-\,\;\.\(\)\&\s-]+?)[^\w\)]+(?=(?:\Z|<tp))");
                authorityStatusNode.ReplaceXmlNodeContentByRegex(matchWholeContentAsAuthoriry, string.Empty, "$1", string.Empty, TaxonAuthorityNodeName, TaxonNodesPrefix, namespaceUri);

                authorityStatusNode.ReplaceXmlNodeByItsInnerXml();
            }
        }

        private void FormatObjectIdInNomenclature(XmlNode nomenclature)
        {
            XmlElement taxonName = nomenclature["tn"];
            if (taxonName != null && nomenclature["object-id"] != null)
            {
                foreach (XmlNode objectId in nomenclature.SelectNodes("object-id"))
                {
                    taxonName.AppendChild(objectId);
                }
            }
        }
    }
}