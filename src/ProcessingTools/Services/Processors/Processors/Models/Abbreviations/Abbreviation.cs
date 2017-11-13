namespace ProcessingTools.Processors.Models.Abbreviations
{
    using System;
    using System.Text.RegularExpressions;
    using System.Xml;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Extensions;
    using ProcessingTools.Processors.Models.Contracts.Abbreviations;

    internal class Abbreviation : IAbbreviation
    {
        private string content;
        private string definition;

        public Abbreviation(string content, string contentType, string definition)
        {
            this.Content = content;
            this.ContentType = contentType;
            this.Definition = definition;
        }

        public Abbreviation(XmlNode abbrev)
        {
            this.SetContent(abbrev);
            this.SetContentType(abbrev);
            this.SetDefinition(abbrev);
        }

        public string Content
        {
            get
            {
                return this.content;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.content = value;
            }
        }

        public string ContentType { get; set; }

        public string Definition
        {
            get
            {
                return this.definition;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.definition = value;
            }
        }

        public string ReplacePattern
        {
            get
            {
                var document = new XmlDocument
                {
                    PreserveWhitespace = true
                };

                var replacePatternNode = document.CreateElement(ElementNames.Abbrev);

                if (!string.IsNullOrEmpty(this.ContentType))
                {
                    var contentTypeAttribute = document.CreateAttribute(AttributeNames.ContentType);
                    contentTypeAttribute.InnerText = this.ContentType;
                    replacePatternNode.Attributes.Append(contentTypeAttribute);
                }

                if (!string.IsNullOrEmpty(this.Definition))
                {
                    var titleAttribute = document.CreateAttribute(
                        Namespaces.XlinkNamespacePrefix,
                        AttributeNames.XLinkTitle,
                        Namespaces.XlinkNamespaceUri);

                    titleAttribute.InnerText = this.Definition;
                    replacePatternNode.Attributes.Append(titleAttribute);
                }

                replacePatternNode.InnerXml = "$1";

                return replacePatternNode.OuterXml;
            }
        }

        public string SearchPattern => $"\\b({Regex.Escape(this.Content)})\\b";

        private void SetContent(XmlNode abbrev)
        {
            var abbrevContent = abbrev.CloneNode(true);
            abbrevContent.SelectNodes(ElementNames.Def).RemoveXmlNodes();
            abbrevContent.SelectNodes("*[name()!='sup'][name()!='sub']").ReplaceXmlNodeByItsInnerXml();

            this.Content = abbrevContent.InnerXml
                .RegexReplace(@"\s+", " ")
                .RegexReplace(@"\A[^\w'""’‘\*\?]+|[^\w'""’‘\*\?]+\Z", string.Empty);
        }

        private void SetContentType(XmlNode abbrev)
        {
            this.ContentType = abbrev.Attributes[AttributeNames.ContentType]?.InnerText;
        }

        private void SetDefinition(XmlNode abbrev)
        {
            this.Definition = abbrev[ElementNames.Def]?.InnerText
                .RegexReplace(@"\s+", " ")
                .RegexReplace(@"\A[=,;:\s–—−-]+|[=,;:\s–—−-]+\Z|\s+(?=\s)", string.Empty)
                .RegexReplace(@"\((.+)\)", "$1")
                .RegexReplace(@"\[(.+)\]", "$1");
        }
    }
}
