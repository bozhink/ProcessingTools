namespace ProcessingTools.BaseLibrary.Abbreviations
{
    using System.Text.RegularExpressions;
    using System.Xml;
    using Globals;

    internal class Abbreviation
    {
        private string contentType;
        private string definition;

        public Abbreviation()
        {
        }

        public Abbreviation(XmlNode abbrev)
        {
            this.Content = abbrev.InnerXml
                .RegexReplace(@"<def.+</def>", string.Empty)
                .RegexReplace(@"</?[ib][^>]*>", string.Empty)
                .RegexReplace(@"\A[^\w'""’‘\*\?]+|[^\w'""’‘\*\?]+\Z", string.Empty);

            this.contentType = abbrev.Attributes["content-type"]?.InnerText;

            var fragment = abbrev.OwnerDocument.CreateDocumentFragment();
            fragment.InnerText = abbrev["def"]?.InnerText
                .RegexReplace(@"\A[=,;:\s–—−-]+|[=,;:\s–—−-]+\Z|\s+(?=\s)", string.Empty);
            this.definition = fragment.InnerXml;
        }

        public string Content { get; set; }

        public string ReplacePattern
        {
            get
            {
                return "<abbrev" +
                    ((this.contentType == null || this.contentType.Length < 1) ? string.Empty : @" content-type=""" + this.contentType + @"""") +
                    ((this.definition == null || this.definition.Length < 1) ? string.Empty : @" xlink:title=""" + this.definition + @"""") +
                    @" xmlns:xlink=""http://www.w3.org/1999/xlink""" +
                    ">$1</abbrev>";
            }
        }

        public string SearchPattern
        {
            get
            {
                return "\\b(" + Regex.Escape(this.Content) + ")\\b";
            }
        }
    }
}