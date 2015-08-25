// 01. Oct. 1930
// 24.- 29.09.1929
// 19/August/2002
// 2012/12/10
// 15th October 2014
// 2nd March 2015
// July 01.2015
// 26–30. June, 2014
// 29th of April, 2015

namespace ProcessingTools.Base
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class DatesTagger : TaggerBase, ITagger
    {
        private TagContent dateTag = new TagContent("date");

        public DatesTagger(Config config, string xml)
            : base(config, xml)
        {
        }

        public DatesTagger(TaggerBase baseObject)
            : base(baseObject)
        {
        }

        public void Tag()
        {
            this.Tag(null);
        }

        public void Tag(IXPathProvider xpathProvider)
        {
            List<string> dates = new List<string>();

            this.TagDayMonthStringYear(dates);
            this.TagDayMonthRomanYear(dates);
            this.TagMonthStringDayYear(dates);
            this.TagDayMonthNumberYear(dates);

            dates = dates.Distinct().ToList();

            {
                string xpathTemplate = "/*";
                if (xpathProvider != null)
                {
                    xpathTemplate = xpathProvider.SelectContentNodesXPathTemplate;
                }

                foreach (string date in dates)
                {
                    Alert.Log(date);
                    TagTextInXmlDocument(date, dateTag, xpathTemplate, true);
                }
            }
        }

        /// <summary>
        /// Finds dates of format DD [mounth as arabic number] YYYY in text and adds them in List dates.
        /// </summary>
        /// <param name="dates">List of string in which to append found dates.</param>
        /// <example>16.6.2013</example>
        private void TagDayMonthNumberYear(List<string> dates)
        {
            string pattern = @"(?<!<[^>]+)((?i)(?:(?:(?:(?:[1-2][0-9]|3[0-1]|0?[1-9])(?:\s*[–—−‒-]\s*))+|(?<![^\s–—−‒-])(?:0[1-9]|[1-2][0-9]|3[0-1]|[1-9]))[^\w<>]{0,4})?\b(?:1[0-2]|0[1-9]|[1-9])\b[^\w<>]{0,4}(?:1[6-9][0-9]|20[0-9])[0-9](?![0-9]))(?![^<>]*>)";
            Regex re = new Regex(pattern);
            this.AddDatesByRegex(dates, re);
        }

        /// <summary>
        /// Finds dates of format [mounth string] DD YYYY in text and adds them in List dates.
        /// </summary>
        /// <param name="dates">List of string in which to append found dates.</param>
        /// <example>March 12.2014</example>
        private void TagMonthStringDayYear(List<string> dates)
        {
            string pattern = @"(?<!<[^>]+)((?i)(?:(?:Jan(?:uary)?|Febr?(?:uary)?|Mar(?:ch)?|Apr(?:il)?|May|June?|July?|Aug(?:ust)?|Sept?(?:ember)?|Oct(?:ober)?|Nov(?:ember)?|Dec(?:ember)?)\s*(?:[–—−‒-]|to)\s*)+(?:(?:(?:(?:[1-2][0-9]|3[0-1]|0?[1-9])(?:\s*[–—−‒-]\s*))+|(?<!\S)(?:[1-2][0-9]|3[0-1]|0?[1-9]))[^\w<>]{0,4})?[^\w<>]{0,4}(?:1[6-9][0-9]|20[0-9])[0-9](?![0-9]))(?![^<>]*>)";
            Regex re = new Regex(pattern);
            this.AddDatesByRegex(dates, re);
        }

        /// <summary>
        /// Finds dates of format DD [mounth in roman] YYYY in text and adds them in List dates.
        /// </summary>
        /// <param name="dates">List of string in which to append found dates.</param>
        /// <example>22–25.I.2007</example>
        private void TagDayMonthRomanYear(List<string> dates)
        {
            ////string pattern = @"((?i)(?:(?:(?:(?:[1-2][0-9]|3[0-1]|0?[1-9])(?:\s*[–—−‒-]\s*))+|(?<!\S)(?:[1-2][0-9]|3[0-1]|0?[1-9]))[^\w<>]{0,4})?(?<![a-z])(?:I|II|III|IV|V|VI|VII|VIII|IX|X|XI|XII)[^\w<>]{0,4}(?:1[6-9][0-9]|20[0-9])[0-9](?![0-9]))";
            string pattern = @"(?<!<[^>]+)((?i)(?:(?:(?:(?:[1-2][0-9]|3[0-1]|0?[1-9])(?:\s*[–—−‒-]\s*))+|(?<![^\s–—−‒-])(?:[1-2][0-9]|3[0-1]|0?[1-9]))[^\w<>]{0,4})?\b(?:I|II|III|IV|V|VI|VII|VIII|IX|X|XI|XII)\b[^\w<>]{0,4}(?:1[6-9][0-9]|20[0-9])[0-9](?![0-9]))(?![^<>]*>)";
            Regex re = new Regex(pattern);
            this.AddDatesByRegex(dates, re);
        }

        /// <summary>
        /// Finds dates of format DD [mounth string] YYYY in text and adds them in List dates.
        /// </summary>
        /// <param name="dates">List of string in which to append found dates.</param>
        /// <example>24–30 March 2013</example>
        /// <example>18 Jan 2008</example>
        private void TagDayMonthStringYear(List<string> dates)
        {
            string pattern = @"(?<!<[^>]+)((?i)(?:(?:(?:(?:[1-2][0-9]|3[0-1]|0?[1-9])(?:\s*[–—−‒-]\s*))+|(?<!\S)(?:[1-2][0-9]|3[0-1]|0?[1-9]))[^\w<>]{0,4})?(?:(?:Jan(?:uary)?|Febr?(?:uary)?|Mar(?:ch)?|Apr(?:il)?|May|June?|July?|Aug(?:ust)?|Sept?(?:ember)?|Oct(?:ober)?|Nov(?:ember)?|Dec(?:ember)?)\s*(?:[–—−‒-]|to)\s*)+[^\w<>]{0,4}(?:1[6-9][0-9]|20[0-9])[0-9](?![0-9]))(?![^<>]*>)";
            Regex re = new Regex(pattern);
            this.AddDatesByRegex(dates, re);
        }

        private void AddDatesByRegex(List<string> dates, Regex re)
        {
            dates.AddRange(this.TextContent.GetMatchesInText(re, true));
        }
    }
}
