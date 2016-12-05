using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Formatters;
using ProcessingTools.Constants.Schema;
using ProcessingTools.Contracts;
using ProcessingTools.Extensions;
using ProcessingTools.Xml.Extensions;

namespace ProcessingTools.Bio.Taxonomy.Processors.Processors.Formatters
{
    public class TreatmentFormatter : ITreatmentFormatter
    {
        private const string TaxonAuthorityStatusNodeName = "AuthorityStatus";

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
            string firstNotWhitespaceNodeInCommentElementXPath = $"{ElementNames.Comment}/node()[normalize-space()!=''][position()=1][name()='{ElementNames.TaxonName}']";
            string xpath = $"//tp:nomenclature-citation[count({ElementNames.Comment}) = count(*)][normalize-space({ElementNames.Comment}) = normalize-space(.)][{firstNotWhitespaceNodeInCommentElementXPath}]";

            document.SelectNodes(xpath)
                .AsParallel()
                .ForAll(citation =>
                {
                    var taxonNode = citation.SelectSingleNode(firstNotWhitespaceNodeInCommentElementXPath);

                    if (taxonNode != null)
                    {
                        citation.PrependChild(taxonNode);
                    }

                    var commentNode = citation.SelectSingleNode(ElementNames.Comment);

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
            XmlNode titleNode = nomenclature.SelectSingleNode(ElementNames.Title);
            if (titleNode != null)
            {
                titleNode.InnerXml = titleNode.InnerXml
                    .RegexReplace("</?i>", string.Empty)
                    .RegexReplace(@"\s+", " ")
                    .Trim();

                var matchLabel = new Regex(@"\A\s*(\S[\s\S]*?)\s*(?=<tn\b)");
                titleNode.ReplaceXmlNodeContentByRegex(matchLabel, string.Empty, "$1", string.Empty, ElementNames.Label);

                var matchAuthority = new Regex(@"(?<=</tn>)\s*(\S[\s\S]+?)\s*\Z");
                titleNode.ReplaceXmlNodeContentByRegex(matchAuthority, string.Empty, "$1", string.Empty, TaxonAuthorityStatusNodeName);

                XmlNode authorityStatusNode = titleNode.SelectSingleNode(TaxonAuthorityStatusNodeName);
                if (authorityStatusNode != null)
                {
                    authorityStatusNode.InnerXml = authorityStatusNode.InnerXml.Trim();

                    var matchWholeContentAsStatus = new Regex(@"(?<=(?:\A|\W\s*))\b([Ii]ncertae\s+[Ss]edis|nom\.?\s+cons\.?|(?:n\.\s*[a-z]+)\b|(?:[a-z]+\.\s*)?spp\b\.?|[a-z]+\.\s*\b(?:n|nov|r|rev)\b\.?|new record)\Z");
                    authorityStatusNode.ReplaceXmlNodeContentByRegex(
                        matchWholeContentAsStatus,
                        string.Empty,
                        "$1",
                        string.Empty,
                        ElementNames.TaxonStatus,
                        Namespaces.TaxPubNamespacePrefix,
                        Namespaces.TaxPubNamespaceUri);

                    var statusNode = authorityStatusNode.SelectSingleNode($"{Namespaces.TaxPubNamespacePrefix}:{ElementNames.TaxonStatus}", document.NamespaceManager);
                    if (statusNode == null)
                    {
                        var authorityNode = authorityStatusNode.OwnerDocument
                            .CreateElement(Namespaces.TaxPubNamespacePrefix, ElementNames.TaxonAuthority, Namespaces.TaxPubNamespaceUri);
                        authorityNode.InnerXml = authorityStatusNode.InnerXml;
                        authorityStatusNode.InnerXml = authorityNode.OuterXml;
                    }
                    else
                    {
                        var matchWholeContentAsAuthoriry = new Regex(@"\A(?!<)([\w\-\,\;\.\(\)\&\s-]+?)[^\w\)]+(?=(?:\Z|<tp))");
                        authorityStatusNode.ReplaceXmlNodeContentByRegex(
                            matchWholeContentAsAuthoriry,
                            string.Empty,
                            "$1",
                            string.Empty,
                            ElementNames.TaxonAuthority,
                            Namespaces.TaxPubNamespacePrefix,
                            Namespaces.TaxPubNamespaceUri);
                    }

                    authorityStatusNode.ReplaceXmlNodeByItsInnerXml();
                }

                titleNode.ReplaceXmlNodeByItsInnerXml();
            }
        }

        private void FormatObjectIdInNomenclature(XmlNode nomenclature)
        {
            XmlElement taxonName = nomenclature[ElementNames.TaxonName];
            if (taxonName != null && nomenclature[ElementNames.ObjectId] != null)
            {
                foreach (XmlNode objectId in nomenclature.SelectNodes(ElementNames.ObjectId))
                {
                    taxonName.AppendChild(objectId);
                }
            }
        }
    }
}
