using System.Text.RegularExpressions;
using System.Xml;

namespace Base
{
    public class DatesTagger : Base
    {
        public DatesTagger()
            : base()
        {
        }

        public DatesTagger(string xml)
            : base(xml)
        {
        }

        public DatesTagger(Config config)
            : base(config)
        {
        }

        public DatesTagger(Config config, string xml)
            : base(config, xml)
        {
        }

        public DatesTagger(Base baseObject)
            : base(baseObject)
        {
        }

        public void TagDates()
        {
            string xpath = "//p|//license-p|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation";

            this.ParseXmlStringToXmlDocument();
            foreach (XmlNode node in this.xmlDocument.SelectNodes(xpath, this.NamespaceManager))
            {
                string replace = node.InnerXml;

                // 18 Jan 2008
                {
                    string pattern = @"(?<!<[^>]+)((?i)(?:(?:(?:(?:[1-2][0-9]|3[0-1]|0?[1-9])(?:\s*[–—−‒-]\s*))+|(?<!\S)(?:[1-2][0-9]|3[0-1]|0?[1-9]))[^\w<>]{0,4})?(?:(?:Jan(?:uary)?|Febr?(?:uary)?|Mar(?:ch)?|Apr(?:il)?|May|June?|July?|Aug(?:ust)?|Sept?(?:ember)?|Oct(?:ober)?|Nov(?:ember)?|Dec(?:ember)?)\s*(?:[–—−‒-]|to)\s*)+[^\w<>]{0,4}(?:1[6-9][0-9]|20[0-9])[0-9](?![0-9]))(?![^<>]*>)";
                    Match m = Regex.Match(replace, pattern);
                    if (m.Success)
                    {
                        // Alert.Message(m.Value);
                        replace = Regex.Replace(replace, pattern, "<date>$1</date>");
                        node.InnerXml = replace;
                    }
                }

                // 22–25.I.2007
                {
                    ////string pattern = @"((?i)(?:(?:(?:(?:[1-2][0-9]|3[0-1]|0?[1-9])(?:\s*[–—−‒-]\s*))+|(?<!\S)(?:[1-2][0-9]|3[0-1]|0?[1-9]))[^\w<>]{0,4})?(?<![a-z])(?:I|II|III|IV|V|VI|VII|VIII|IX|X|XI|XII)[^\w<>]{0,4}(?:1[6-9][0-9]|20[0-9])[0-9](?![0-9]))";
                    string pattern = @"(?<!<[^>]+)((?i)(?:(?:(?:(?:[1-2][0-9]|3[0-1]|0?[1-9])(?:\s*[–—−‒-]\s*))+|(?<![^\s–—−‒-])(?:[1-2][0-9]|3[0-1]|0?[1-9]))[^\w<>]{0,4})?\b(?:I|II|III|IV|V|VI|VII|VIII|IX|X|XI|XII)\b[^\w<>]{0,4}(?:1[6-9][0-9]|20[0-9])[0-9](?![0-9]))(?![^<>]*>)";
                    Match m = Regex.Match(replace, pattern);
                    if (m.Success)
                    {
                        // Alert.Message(m.Value);
                        replace = Regex.Replace(replace, pattern, "<date>$1</date>");
                        node.InnerXml = replace;
                    }
                }

                // March 12.2014
                {
                    string pattern = @"(?<!<[^>]+)((?i)(?:(?:Jan(?:uary)?|Febr?(?:uary)?|Mar(?:ch)?|Apr(?:il)?|May|June?|July?|Aug(?:ust)?|Sept?(?:ember)?|Oct(?:ober)?|Nov(?:ember)?|Dec(?:ember)?)\s*(?:[–—−‒-]|to)\s*)+(?:(?:(?:(?:[1-2][0-9]|3[0-1]|0?[1-9])(?:\s*[–—−‒-]\s*))+|(?<!\S)(?:[1-2][0-9]|3[0-1]|0?[1-9]))[^\w<>]{0,4})?[^\w<>]{0,4}(?:1[6-9][0-9]|20[0-9])[0-9](?![0-9]))(?![^<>]*>)";
                    Match m = Regex.Match(replace, pattern);
                    if (m.Success)
                    {
                        // Alert.Message(m.Value);
                        replace = Regex.Replace(replace, pattern, "<date>$1</date>");
                        node.InnerXml = replace;
                    }
                }
            }

            this.ParseXmlDocumentToXmlString();
        }
    }
}
