namespace ProcessingTools.BaseLibrary.Abbreviations
{
    using System.Text.RegularExpressions;
    using System.Xml;
    using Extensions;

    internal class Abbreviation
    {
        private string contentType;
        private string definition;

        public Abbreviation(XmlNode abbrev)
        {
            this.SetContent(abbrev);
            this.SetContentType(abbrev);
            this.SetDefinition(abbrev);
            this.SetReplacePattern(abbrev);
        }

        public string Content { get; private set; }

        public string ReplacePattern { get; private set; }

        public string SearchPattern
        {
            get
            {
                return $"\\b({Regex.Escape(this.Content)})\\b";
            }
        }

        private void SetContent(XmlNode abbrev)
        {
            var abbrevContent = abbrev.CloneNode(true);
            abbrevContent.SelectNodes("def").RemoveXmlNodes();
            abbrevContent.SelectNodes("i|b").ReplaceXmlNodeByItsInnerXml();

            this.Content = abbrevContent.InnerXml
                .RegexReplace(@"\A[^\w'""’‘\*\?]+|[^\w'""’‘\*\?]+\Z", string.Empty);
        }

        private void SetContentType(XmlNode abbrev)
        {
            this.contentType = abbrev.Attributes["content-type"]?.InnerText;
        }

        private void SetDefinition(XmlNode abbrev)
        {
            this.definition = abbrev["def"]?.InnerText
                .RegexReplace(@"\A[=,;:\s–—−-]+|[=,;:\s–—−-]+\Z|\s+(?=\s)", string.Empty)
                .RegexReplace(@"\((.+)\)", "$1")
                .RegexReplace(@"\[(.+)\]", "$1");
        }

        private void SetReplacePattern(XmlNode abbrev)
        {
            var replacePatternNode = abbrev.OwnerDocument.CreateElement("abbrev");

            if (!string.IsNullOrEmpty(this.contentType))
            {
                var contentTypeAttribute = abbrev.OwnerDocument.CreateAttribute("content-type");
                contentTypeAttribute.InnerText = this.contentType;
                replacePatternNode.Attributes.Append(contentTypeAttribute);
            }

            if (!string.IsNullOrEmpty(this.definition))
            {
                var titleAttribute = abbrev.OwnerDocument.CreateAttribute("xlink", "title", "http://www.w3.org/1999/xlink");
                titleAttribute.InnerText = this.definition;
                replacePatternNode.Attributes.Append(titleAttribute);
            }

            replacePatternNode.InnerXml = "$1";

            this.ReplacePattern = replacePatternNode.OuterXml;
        }
    }
}