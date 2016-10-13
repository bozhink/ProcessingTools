namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;
    using ProcessingTools.Xml.Extensions;

    public class TreatmentFormatter : IDocumentFormatter
    {
        private const string TitleNodeName = "title";
        private const string LabelNodeName = "label";
        private const string TaxonAuthorityStatusNodeName = "AuthorityStatus";
        private const string TaxonNodesPrefix = "tp";
        private const string TaxonAuthorityNodeName = "taxon-authority";
        private const string TaxonStatusNodeName = "taxon-status";

        private const string TaxonNameElementName = "tn";
        private const string ObjectIdNodeName = "object-id";

        private const string CommentElementName = "comment";

        private ILogger logger;

        public TreatmentFormatter(ILogger logger)
        {
            this.logger = logger;
        }

        public Task<object> Format(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            return Task.Run<object>(() =>
            {
                try
                {
                    this.RemoveWrappingItalicsOfTaxonNamesInNomenclatureCitations(document);
                    this.FormatNomenclatureCitations(document);
                    this.FormatNomenclaturesWithTitle(document);
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, string.Empty);
                }

                return true;
            });
        }

        private void RemoveWrappingItalicsOfTaxonNamesInNomenclatureCitations(IDocument document)
        {
            const string XPath = "//tp:nomenclature-citation//i[count(tn) = count(*)][normalize-space(tn) = normalize-space(.)]";

            document.SelectNodes(XPath)
                .AsParallel()
                .ForAll(italicNode => italicNode.ReplaceXmlNodeByItsInnerXml());
        }

        private void FormatNomenclatureCitations(IDocument document)
        {
            string firstNotWhitespaceNodeInCommentElementXPath = $"{CommentElementName}/node()[normalize-space()!=''][position()=1][name()='{TaxonNameElementName}']";
            string xpath = $"//tp:nomenclature-citation[count({CommentElementName}) = count(*)][normalize-space({CommentElementName}) = normalize-space(.)][{firstNotWhitespaceNodeInCommentElementXPath}]";

            document.SelectNodes(xpath)
                .AsParallel()
                .ForAll(citation =>
                {
                    var taxonNode = citation.SelectSingleNode(firstNotWhitespaceNodeInCommentElementXPath);

                    if (taxonNode != null)
                    {
                        citation.PrependChild(taxonNode);
                    }

                    var commentNode = citation.SelectSingleNode(CommentElementName);

                    if (commentNode != null)
                    {
                        commentNode.InnerXml = commentNode.InnerXml.Trim();

                        if (string.IsNullOrWhiteSpace(commentNode.InnerXml))
                        {
                            commentNode.ParentNode.RemoveChild(commentNode);
                        }
                    }
                });
        }

        private void FormatNomenclaturesWithTitle(IDocument document)
        {
            const string XPath = "//tp:nomenclature[title]";

            document.SelectNodes(XPath)
                .AsParallel()
                .ForAll(nomenclature =>
                {
                    nomenclature.SelectNodes(".//i[tn]").ReplaceXmlNodeByItsInnerXml();

                    this.FormatNomencatureContent(document, nomenclature);
                    this.FormatObjectIdInNomenclature(nomenclature);
                });
        }

        private void FormatNomencatureContent(IDocument document, XmlNode nomenclature)
        {
            string namespaceUri = document.NamespaceManager.LookupNamespace(TaxonNodesPrefix);

            XmlNode titleNode = nomenclature.SelectSingleNode(TitleNodeName);
            if (titleNode != null)
            {
                titleNode.InnerXml = titleNode.InnerXml
                    .RegexReplace("</?i>", string.Empty)
                    .RegexReplace(@"\s+", " ")
                    .Trim();

                var matchLabel = new Regex(@"\A\s*(\S[\s\S]*?)\s*(?=<tn\b)");
                titleNode.ReplaceXmlNodeContentByRegex(matchLabel, string.Empty, "$1", string.Empty, LabelNodeName);

                var matchAuthority = new Regex(@"(?<=</tn>)\s*(\S[\s\S]+?)\s*\Z");
                titleNode.ReplaceXmlNodeContentByRegex(matchAuthority, string.Empty, "$1", string.Empty, TaxonAuthorityStatusNodeName);

                XmlNode authorityStatusNode = titleNode.SelectSingleNode(TaxonAuthorityStatusNodeName);
                if (authorityStatusNode != null)
                {
                    authorityStatusNode.InnerXml = authorityStatusNode.InnerXml.Trim();

                    var matchWholeContentAsStatus = new Regex(@"(?<=(?:\A|\W\s*))\b([Ii]ncertae\s+[Ss]edis|nom\.?\s+cons\.?|(?:n\.\s*[a-z]+)\b|(?:[a-z]+\.\s*)?spp\b\.?|[a-z]+\.\s*\b(?:n|nov|r|rev)\b\.?|new record)\Z");
                    authorityStatusNode.ReplaceXmlNodeContentByRegex(matchWholeContentAsStatus, string.Empty, "$1", string.Empty, TaxonStatusNodeName, TaxonNodesPrefix, namespaceUri);

                    var statusNode = authorityStatusNode.SelectSingleNode($"{TaxonNodesPrefix}:{TaxonStatusNodeName}", document.NamespaceManager);
                    if (statusNode == null)
                    {
                        var authorityNode = authorityStatusNode.OwnerDocument.CreateElement(TaxonNodesPrefix, TaxonAuthorityNodeName, namespaceUri);
                        authorityNode.InnerXml = authorityStatusNode.InnerXml;
                        authorityStatusNode.InnerXml = authorityNode.OuterXml;
                    }
                    else
                    {
                        var matchWholeContentAsAuthoriry = new Regex(@"\A(?!<)([\w\-\,\;\.\(\)\&\s-]+?)[^\w\)]+(?=(?:\Z|<tp))");
                        authorityStatusNode.ReplaceXmlNodeContentByRegex(matchWholeContentAsAuthoriry, string.Empty, "$1", string.Empty, TaxonAuthorityNodeName, TaxonNodesPrefix, namespaceUri);
                    }

                    authorityStatusNode.ReplaceXmlNodeByItsInnerXml();
                }

                titleNode.ReplaceXmlNodeByItsInnerXml();
            }
        }

        private void FormatObjectIdInNomenclature(XmlNode nomenclature)
        {
            XmlElement taxonName = nomenclature[TaxonNameElementName];
            if (taxonName != null && nomenclature[ObjectIdNodeName] != null)
            {
                foreach (XmlNode objectId in nomenclature.SelectNodes(ObjectIdNodeName))
                {
                    taxonName.AppendChild(objectId);
                }
            }
        }
    }
}