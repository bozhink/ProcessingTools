namespace ProcessingTools.BaseLibrary.Abbreviations
{
    using System.Text.RegularExpressions;

    internal class Abbreviation
    {
        public string Content { get; set; }

        public string ContentType { get; set; }

        public string Definition { get; set; }

        public string ReplacePattern
        {
            get
            {
                return "<abbrev" +
                    ((this.ContentType == null || this.ContentType == string.Empty) ? string.Empty : @" content-type=""" + this.ContentType + @"""") +
                    ((this.Definition == null || this.Definition == string.Empty) ? string.Empty : @" xlink:title=""" + this.Definition + @"""") +
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