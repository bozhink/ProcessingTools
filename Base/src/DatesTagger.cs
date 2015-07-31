using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace ProcessingTools.Base
{
    public class DatesTagger : Base
    {
        public DatesTagger(Config config, string xml)
            : base(config, xml)
        {
        }

        public DatesTagger(Base baseObject)
            : base(baseObject)
        {
        }

        public void TagDates(IXPathProvider xpathProvider)
        {
            List<string> dates = new List<string>();

            this.ParseXmlStringToXmlDocument();
            XmlNodeList nodeList = this.xmlDocument.SelectNodes(xpathProvider.SelectContentNodesXPath, this.NamespaceManager);
            {
                // 18 Jan 2008
                {
                    string pattern = @"(?<!<[^>]+)((?i)(?:(?:(?:(?:[1-2][0-9]|3[0-1]|0?[1-9])(?:\s*[–—−‒-]\s*))+|(?<!\S)(?:[1-2][0-9]|3[0-1]|0?[1-9]))[^\w<>]{0,4})?(?:(?:Jan(?:uary)?|Febr?(?:uary)?|Mar(?:ch)?|Apr(?:il)?|May|June?|July?|Aug(?:ust)?|Sept?(?:ember)?|Oct(?:ober)?|Nov(?:ember)?|Dec(?:ember)?)\s*(?:[–—−‒-]|to)\s*)+[^\w<>]{0,4}(?:1[6-9][0-9]|20[0-9])[0-9](?![0-9]))(?![^<>]*>)";
                    Regex re = new Regex(pattern);
                    dates.AddRange(GetMatchesInXmlText(nodeList, re, true));
                }

                // 22–25.I.2007
                {
                    ////string pattern = @"((?i)(?:(?:(?:(?:[1-2][0-9]|3[0-1]|0?[1-9])(?:\s*[–—−‒-]\s*))+|(?<!\S)(?:[1-2][0-9]|3[0-1]|0?[1-9]))[^\w<>]{0,4})?(?<![a-z])(?:I|II|III|IV|V|VI|VII|VIII|IX|X|XI|XII)[^\w<>]{0,4}(?:1[6-9][0-9]|20[0-9])[0-9](?![0-9]))";
                    string pattern = @"(?<!<[^>]+)((?i)(?:(?:(?:(?:[1-2][0-9]|3[0-1]|0?[1-9])(?:\s*[–—−‒-]\s*))+|(?<![^\s–—−‒-])(?:[1-2][0-9]|3[0-1]|0?[1-9]))[^\w<>]{0,4})?\b(?:I|II|III|IV|V|VI|VII|VIII|IX|X|XI|XII)\b[^\w<>]{0,4}(?:1[6-9][0-9]|20[0-9])[0-9](?![0-9]))(?![^<>]*>)";
                    Regex re = new Regex(pattern);
                    dates.AddRange(GetMatchesInXmlText(nodeList, re, true));
                }

                // March 12.2014
                {
                    string pattern = @"(?<!<[^>]+)((?i)(?:(?:Jan(?:uary)?|Febr?(?:uary)?|Mar(?:ch)?|Apr(?:il)?|May|June?|July?|Aug(?:ust)?|Sept?(?:ember)?|Oct(?:ober)?|Nov(?:ember)?|Dec(?:ember)?)\s*(?:[–—−‒-]|to)\s*)+(?:(?:(?:(?:[1-2][0-9]|3[0-1]|0?[1-9])(?:\s*[–—−‒-]\s*))+|(?<!\S)(?:[1-2][0-9]|3[0-1]|0?[1-9]))[^\w<>]{0,4})?[^\w<>]{0,4}(?:1[6-9][0-9]|20[0-9])[0-9](?![0-9]))(?![^<>]*>)";
                    Regex re = new Regex(pattern);
                    dates.AddRange(GetMatchesInXmlText(nodeList, re, true));
                }

                // 16.6.2013
                {
                    string pattern = @"(?<!<[^>]+)((?i)(?:(?:(?:(?:[1-2][0-9]|3[0-1]|0?[1-9])(?:\s*[–—−‒-]\s*))+|(?<![^\s–—−‒-])(?:[1-2][0-9]|3[0-1]|0?[1-9]))[^\w<>]{0,4})?\b(?:1[0-2]?|0?[2-9])\b[^\w<>]{0,4}(?:1[6-9][0-9]|20[0-9])[0-9](?![0-9]))(?![^<>]*>)";
                    Regex re = new Regex(pattern);
                    dates.AddRange(GetMatchesInXmlText(nodeList, re, true));
                }

                // 2012/12/10
                // 15th October 2014
                // 2nd March 2015
                // July 01.2015
                // 26–30. June, 2014
                // 29th of April, 2015

                dates = dates.Distinct().ToList();
            }

            TagContent dateTag = new TagContent("date");
            foreach (string date in dates)
            {
                Alert.Log(date);
                TagTextInXmlDocument(date, dateTag, xpathProvider.SelectContentNodesXPathTemplate, true);
            }

            this.ParseXmlDocumentToXmlString();
        }
    }
}
