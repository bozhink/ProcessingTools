namespace ProcessingTools.Processors.Processors.Bio.Taxonomy.Formatters
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Formatters;

    public class TreatmentFormatter : ITreatmentFormatter
    {
        private const string TaxonAuthorityStatusNodeName = "AuthorityStatus";

        public Task<object> FormatAsync(IDocument context) => Task.Run(() => this.FormatSync(context));

        private object FormatSync(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            this.RemoveWrappingItalicsOfTaxonNamesInNomenclatureCitations(document);
            this.FormatNomenclatureCitations(document);
            this.FormatNomenclaturesWithTitle(document);

            return true;
        }

        private void CleanTitleNodeContent(XmlNode titleNode)
        {
            if (titleNode == null)
            {
                return;
            }

            titleNode.InnerXml = titleNode.InnerXml
                .RegexReplace("</?i>", string.Empty)
                .RegexReplace(@"\s+", " ")
                .Trim();
        }

        private void FormatNomencatureContent(IDocument document, XmlNode titleNode)
        {
            if (titleNode == null)
            {
                return;
            }

            this.CleanTitleNodeContent(titleNode);

            var taxonNameNode = this.SelectTaxonNameNode(titleNode);

            this.TagLabel(titleNode, taxonNameNode);
            this.TagAuthorityStatus(titleNode, taxonNameNode);

            this.ProcessAuthorityStatusNode(document, this.SelectAuthorityStatusNode(titleNode));

            titleNode.ReplaceXmlNodeByItsInnerXml();
        }

        private void FormatNomenclatureCitations(IDocument document)
        {
            string firstNotWhitespaceNodeInCommentElementXPath = $"{ElementNames.Comment}/node()[normalize-space()!=''][position()=1][name()='{ElementNames.TaxonName}']";
            string xpath = $".//tp:nomenclature-citation[count({ElementNames.Comment}) = count(*)][normalize-space({ElementNames.Comment}) = normalize-space(.)][{firstNotWhitespaceNodeInCommentElementXPath}]";

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
            const string XPath = ".//tp:nomenclature[title]";

            document.SelectNodes(XPath)
                .AsParallel()
                .ForAll(nomenclature =>
                {
                    nomenclature.SelectNodes(".//i[tn]").ReplaceXmlNodeByItsInnerXml();
                    this.FormatNomencatureContent(document, this.SelectTitleNode(nomenclature));
                    this.FormatObjectIdInNomenclature(nomenclature);
                });
        }

        private void FormatObjectIdInNomenclature(XmlNode nomenclature)
        {
            if (nomenclature == null)
            {
                return;
            }

            XmlElement taxonName = nomenclature[ElementNames.TaxonName];
            if (taxonName != null && nomenclature[ElementNames.ObjectId] != null)
            {
                foreach (XmlNode objectId in nomenclature.SelectNodes(ElementNames.ObjectId))
                {
                    taxonName.AppendChild(objectId);
                }
            }
        }

        private void ProcessAuthorityStatusNode(IDocument document, XmlNode authorityStatusNode)
        {
            if (authorityStatusNode == null)
            {
                return;
            }

            authorityStatusNode.InnerXml = authorityStatusNode.InnerXml.Trim();

            if (string.IsNullOrWhiteSpace(authorityStatusNode.InnerXml))
            {
                return;
            }

            var matchRightPartAsStatus = new Regex(@"(?<=(?:\A|\W\s*))\b([Ii]ncertae\s+[Ss]edis|nom\.?\s+cons\.?|new record|(?:n\.\s*[a-z]+)\b|(?:[a-z]+\.\s*)?spp\b\.?|[a-z]+\.\s*\b(?:n|nov|r|rev|stat)\b\.?)\Z");
            authorityStatusNode.ReplaceXmlNodeContentByRegex(
                matchRightPartAsStatus,
                Tuple.Create(string.Empty, "$1", string.Empty),
                ElementNames.TaxonStatus,
                Namespaces.TaxPubNamespacePrefix,
                Namespaces.TaxPubNamespaceUri);

            var statusNode = this.SelectTaxonStatusNode(document, authorityStatusNode);
            if (statusNode == null)
            {
                var authorityNode = authorityStatusNode.OwnerDocument.CreateElement(
                    Namespaces.TaxPubNamespacePrefix,
                    ElementNames.TaxonAuthority,
                    Namespaces.TaxPubNamespaceUri);

                authorityNode.InnerXml = authorityStatusNode.InnerXml;
                authorityStatusNode.InnerXml = authorityNode.OuterXml;
            }
            else
            {
                statusNode.InnerXml = statusNode.InnerXml.RegexReplace(@"\s+", " ").Trim();

                var matchAuthoriry = new Regex(@"\A([\s\S]+)(?=" + Regex.Escape(statusNode.OuterXml) + @")");
                authorityStatusNode.ReplaceXmlNodeContentByRegex(
                    matchAuthoriry,
                    Tuple.Create(string.Empty, "$1", string.Empty),
                    ElementNames.TaxonAuthority,
                    Namespaces.TaxPubNamespacePrefix,
                    Namespaces.TaxPubNamespaceUri);

                var authorityNode = this.SelectTaxonAuthorityNode(document, authorityStatusNode);
                if (authorityNode != null)
                {
                    authorityNode.InnerXml = authorityNode.InnerXml.RegexReplace(@"\s+", " ").Trim().TrimEnd(new[] { ',' });
                }
            }

            authorityStatusNode.ReplaceXmlNodeByItsInnerXml();
        }

        private void RemoveWrappingItalicsOfTaxonNamesInNomenclatureCitations(IDocument document)
        {
            const string XPath = ".//tp:nomenclature-citation//i[count(tn) = count(*)][normalize-space(tn) = normalize-space(.)]";

            document.SelectNodes(XPath)
                .AsParallel()
                .ForAll(italicNode => italicNode.ReplaceXmlNodeByItsInnerXml());
        }

        private XmlNode SelectAuthorityStatusNode(XmlNode node)
        {
            return node.SelectSingleNode(TaxonAuthorityStatusNodeName);
        }

        private XmlNode SelectLabelNode(XmlNode node)
        {
            return node.SelectSingleNode(ElementNames.Label);
        }

        private XmlNode SelectTaxonAuthorityNode(IDocument document, XmlNode node)
        {
            return node.SelectSingleNode($"{Namespaces.TaxPubNamespacePrefix}:{ElementNames.TaxonAuthority}", document.NamespaceManager);
        }

        private XmlNode SelectTaxonNameNode(XmlNode node)
        {
            return node.SelectSingleNode(ElementNames.TaxonName);
        }

        private XmlNode SelectTaxonStatusNode(IDocument document, XmlNode node)
        {
            return node.SelectSingleNode($"{Namespaces.TaxPubNamespacePrefix}:{ElementNames.TaxonStatus}", document.NamespaceManager);
        }

        private XmlNode SelectTitleNode(XmlNode node)
        {
            return node.SelectSingleNode(ElementNames.Title);
        }

        private void TagAuthorityStatus(XmlNode titleNode, XmlNode taxonNameNode)
        {
            if (titleNode == null || taxonNameNode == null)
            {
                return;
            }

            var matchAuthorityStatus = new Regex(@"(?<=" + Regex.Escape(taxonNameNode.OuterXml) + @")\s*(\S[\s\S]*?)\s*\Z");
            titleNode.ReplaceXmlNodeContentByRegex(matchAuthorityStatus, Tuple.Create(string.Empty, "$1", string.Empty), TaxonAuthorityStatusNodeName);

            var authorityStatusNode = this.SelectAuthorityStatusNode(titleNode);
            if (authorityStatusNode != null)
            {
                authorityStatusNode.InnerXml = authorityStatusNode.InnerXml.RegexReplace(@"\s+", " ").Trim();
            }
        }

        private void TagLabel(XmlNode titleNode, XmlNode taxonNameNode)
        {
            if (titleNode == null || taxonNameNode == null)
            {
                return;
            }

            var matchLabel = new Regex(@"\A\s*(\S[\s\S]*?)\s*(?=" + Regex.Escape(taxonNameNode.OuterXml) + @")");
            titleNode.ReplaceXmlNodeContentByRegex(matchLabel, Tuple.Create(string.Empty, "$1", string.Empty), ElementNames.Label);

            var labelNode = this.SelectLabelNode(titleNode);
            if (labelNode != null)
            {
                labelNode.InnerXml = labelNode.InnerXml.RegexReplace(@"\s+", " ").Trim();
            }
        }
    }
}
