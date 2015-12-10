namespace ProcessingTools.BaseLibrary.References
{
    using System;
    using System.Text.RegularExpressions;
    using System.Xml;

    using Configurator;
    using Contracts;
    using ProcessingTools.Contracts.Log;

    public class ReferencesParser : TaggerBase, IBaseParser
    {
        private ILogger logger;

        public ReferencesParser(Config config, string xml, ILogger logger)
            : base(config, xml)
        {
            this.logger = logger;
        }

        public ReferencesParser(IBase baseObject, ILogger logger)
            : base(baseObject)
        {
            this.logger = logger;
        }

        public void Parse()
        {
            try
            {
                XmlNodeList nodeList = this.XmlDocument.SelectNodes("//element-citation|//mixed-citation|nlm-citation", this.NamespaceManager);
                foreach (XmlNode node in nodeList)
                {
                    node.InnerXml = this.ReferencePartSplitter(node);
                    node.InnerXml = this.ReferenceJournalMatch(node);
                    node.InnerXml = this.ReferencePersonGroupSplit(node.InnerXml);
                }
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }
        }

        private string ReferencePartSplitter(XmlNode reference)
        {
            string result = reference.InnerXml;
            result = Regex.Replace(
                result,
                @"(?i)(.*?)\s*\(\s*(eds?\.?)\s*\)\s*\(\s*(\d{4}[\w\.]?|in pre[ps\.]+|submitted|\d{4}.\d{4}|\d{4}.\d{2}|\d{4}\W*\[\d{4}\])(.*?)\s*\)\s*(.*)\s*",
                "<person-group>$1</person-group> (<role>$2</role>) (<year>$3</year>$4) <fake_tag>$5</fake_tag>");

            result = Regex.Replace(
                result,
                @"(?i)(.*?)\s*\(\s*(\d{4}[\w\.]?|in pre[ps\.]+|submitted|\d{4}.\d{4}|\d{4}.\d{2}|\d{4}\W*\[\d{4}\])(.*?)\s*\)\s*(.*)\s*",
                "<person-group>$1</person-group> (<year>$2</year>$3) <fake_tag>$4</fake_tag>");

            result = Regex.Replace(
                result,
                @"(?i)\s*(((\[\s*in.*?\]|\bdoi\W\s*\S+|\bhttps?\S+|\[\s*access.*?\]|\(\s*access.*?\))[,;\.\s]*)+)(</fake_tag>)",
                "$4 <fake_tag_1>$1</fake_tag_1>");

            return result;
        }

        private string ReferenceJournalMatch(XmlNode reference)
        {
            string result = reference.InnerXml;
            XmlNodeList nodeList = reference.SelectNodes("//fake_tag_1");
            foreach (XmlNode node in nodeList)
            {
                node.InnerXml = Regex.Replace(node.InnerXml, @"(?<=\[)\s*(access.*?)\s*(?=\])", "[<date-in-citation content-type=\"access-date\">$1</date-in-citation>]");
                node.InnerXml = Regex.Replace(node.InnerXml, @"(?<=\()\s*(access.*?)\s*(?=\))", "[<date-in-citation content-type=\"access-date\">$1</date-in-citation>]");
                node.InnerXml = Regex.Replace(node.InnerXml, @"\[\s*(in.*?)\s*\]", "<comment>[$1]</comment>");
            }

            reference.InnerXml = Regex.Replace(reference.InnerXml, "</?fake_tag_1>", string.Empty);

            nodeList = reference.SelectNodes("//fake_tag");
            foreach (XmlNode node in nodeList)
            {
                // Reference journal match
                /*
                 * Reference citation with role with link
                 * /<fake_tag>(.*?)\s*In:([^\.]+)\s*\((Ed|ed|Ed\.|ed\.|Eds|eds|Eds\.|eds\.)\)\.?(.*?\.)\s*([^\.\d]+)\s*(\d+)\s*:\s*(\d+)\s*[-–](\d+)(\s*\.\s*)(.+)<\/fake_tag>/umis
                 * <article-title>$1</article-title> In: <person-group1>$2</person-group1> (<role>$3</role>). <issue-title>$4</issue-title> <source>$5</source> <volume>$6</volume>: <fpage>$7</fpage>–<lpage>$8</lpage>$9$10
                 */
                node.InnerXml = Regex.Replace(
                    node.InnerXml,
                    @"^(?i)(.*?)\s*\bin:\s*([^\.]+)\s*\(\s*(ed|ed\.|eds|eds\.)\s*\)\.?\s*(.*?\.)\s*([^\.\d]+)\s*(\d+)\s*:\s*(\d+)\s*[-–](\d+)\s*(\..*)$",
                    "<article-title>$1</article-title> In: <person-group>$2</person-group> (<role>$3</role>) <issue-title>$4</issue-title> <source>$5</source> <volume>$6</volume>: <fpage>$7</fpage>–<lpage>$8</lpage>$9");
                /*
                 * Reference citation with role without link
                 * /<fake_tag>(.*?)\s*In:([^\.]+)\s*\((Ed|ed|Ed\.|ed\.|Eds|eds|Eds\.|eds\.)\)\.?(.*?\.)\s*([^\.\d]+)(\s+)(\d+)\s*:\s*(\d+)\s*[-–](\d+)(\s*\.?\s*)<\/fake_tag>/umis
                 * <article-title>$1</article-title> In: <person-group1>$2</person-group1> (<role>$3</role>). <issue-title>$4</issue-title> <source>$5</source> <volume>$7</volume>: <fpage>$8</fpage>-<lpage>$9</lpage>$10
                 */
                node.InnerXml = Regex.Replace(
                    node.InnerXml,
                    @"^(?i)(.*?)\s*\bin:\s*([^\.]+)\s*\(\s*(ed|ed\.|eds|eds\.)\s*\)\.?\s*(.*?\.)\s*([^\.\d]+)\s*(\d+)\s*:\s*(\d+)\s*[-–](\d+)\s*\.?\s*$",
                    "<article-title>$1</article-title> In: <person-group>$2</person-group> (<role>$3</role>) <issue-title>$4</issue-title> <source>$5</source> <volume>$6</volume>: <fpage>$7</fpage>–<lpage>$8</lpage>.");
                /*
                 * Reference citation without role without link
                 * /<fake_tag>(.*?)\s*In:\s*([^\.]+)\s*\.(.*?\.)\s*([^\.\d]+)\s*(\d+)\s*:\s*(\d+)\s*[-–](\d+)(\s*\.?\s*)<\/fake_tag>/umis
                 * <article-title>$1</article-title> In: <person-group1>$2</person-group1>. <issue-title>$3</issue-title> <source>$4</source> <volume>$5</volume>: <fpage>$6</fpage>-<lpage>$7</lpage>$8
                 */
                node.InnerXml = Regex.Replace(
                    node.InnerXml,
                    @"^(?i)(.*?)\s*\bin:\s*([^\.]+)\s*\.\s*(.*?\.)\s*([^\.\d]+)\s*(\d+)\s*:\s*(\d+)\s*[-–](\d+)\s*\.?\s*$",
                    "<article-title>$1</article-title> In: <person-group>$2</person-group>. <issue-title>$3</issue-title> <source>$4</source> <volume>$5</volume>: <fpage>$6</fpage>–<lpage>$7</lpage>.");
                /*
                 * Reference citation with role without volume, only pages
                 * /<fake_tag>(.*?)\s*In:([^\.]+)\s*\((Ed|ed|Ed.|ed.|Eds|eds|Eds.|eds.)\)\s*\.?(.*?\.)\s*(.*?)\s*[:,]\s*(\d+)\s*[-–](\d+)(\s*\.?\s*)<\/fake_tag>/umis
                 * 
                 */
                node.InnerXml = Regex.Replace(
                    node.InnerXml,
                    @"^(?i)(.*?)\s*\bin:\s*([^\.]+)\s*\(\s*(ed|ed\.|eds|eds\.)\s*\)\s*\.?\s*(.*?\.)\s*(.*?)\s*([:,])\s*(\d+)\s*[-–]\s*(\d+)\s*\.?\s*$",
                    "<article-title>$1</article-title> In: <person-group>$2</person-group> (<role>$3</role>) <issue-title>$4</issue-title> <source>$5</source>$6 <fpage>$7</fpage>–<lpage>$8</lpage>.");
                /*
                 * Reference citation without role with link
                 * /<fake_tag>(.*?)\s*In:\s*([^\.]+)\s*\.(.*?\.)\s*([^\.\d]+)\s*(\d+)\s*:\s*(\d+)\s*[-–](\d+)(\s*\.?\s*)(.+)<\/fake_tag>/umis
                 * <article-title>$1</article-title> In: <person-group1>$2</person-group1>. <issue-title>$3</issue-title> <source>$4</source> <volume>$5</volume>: <fpage>$6</fpage>-<lpage>$7</lpage>$8$9
                 */
                node.InnerXml = Regex.Replace(
                    node.InnerXml,
                    @"^(?i)(.*?)\s*\bin:\s*([^\.]+)\s*\.\s*(.*?\.)\s*([^\.\d]+)\s*(\d+)\s*:\s*(\d+)\s*[-–]\s*(\d+)\s*(\.?\s*.+)$",
                    "<article-title>$1</article-title> In: <person-group>$2</person-group>. <issue-title>$3</issue-title> <source>$4</source> <volume>$5</volume>: <fpage>$6</fpage>–<lpage>$7</lpage>$8");
                /*
                 * Reference journal with long issue
                 * /<fake_tag>(.*?\.)\s*([^\.\d]+)\s*(\d+)\s*(\(|\[)(\d+[-–]\d+)(\)|\])\s*:\s*(\d+)\s*[-–](\d+)(\s*\.?\s*)<\/fake_tag>/umis
                 * <article-title>$1</article-title> <source>$2</source> <volume>$3</volume>$4<issue>$5</issue>)$6: <fpage>$7</fpage>-<lpage>$8</lpage>$9
                 */
                node.InnerXml = Regex.Replace(
                    node.InnerXml,
                    @"^(?i)(.+(?<!>\w{1,3})[\.\?])\s*([^\.\d]+?)\s*(\d+)\s*([\(\[]\d+[-–]\d+[\)\]])\s*:\s*(\d+)\s*[-–]\s*(\d+)\s*\.?\s*$",
                    "<article-title>$1</article-title> <source>$2</source> <volume>$3</volume><issue>$4</issue>: <fpage>$5</fpage>–<lpage>$6</lpage>.");
                /*
                 * Reference journal with issue
                 * /<fake_tag>(.*?\.)\s*([^\.\d]+)\s*(\d+)\s*(\(|\[)(\d+)(\)|\])\s*:\s*(\d+)\s*[-–](\d+)(\s*\.?\s*)<\/fake_tag>/umis
                 * <article-title>$1</article-title> <source>$2</source> <volume>$3</volume> $4<issue>$5</issue>$6: <fpage>$7</fpage>-<lpage>$8</lpage>$9
                 */
                node.InnerXml = Regex.Replace(
                    node.InnerXml,
                    @"^(?i)(.+(?<!>\w{1,3})[\.\?])\s*([^\.\d]+?)\s*(\d+)\s*([\(\[]\d+[\)\]])\s*:\s*(\d+)\s*[-–](\d+)\s*\.?\s*$",
                    "<article-title>$1</article-title> <source>$2</source> <volume>$3</volume><issue>$4</issue>: <fpage>$5</fpage>–<lpage>$6</lpage>.");
                /*
                 * Reference journal without issue
                 * /<fake_tag>(.*?\.)\s*([^\.\d]+)\s*(\d+)\s*:\s*(\d+)\s*[-–](\d+)(\s*\.?\s*)<\/fake_tag>/umis
                 * <article-title>$1</article-title> <source>$2</source> <volume>$3</volume>: <fpage>$4</fpage>-<lpage>$5</lpage>$6
                 */
                node.InnerXml = Regex.Replace(
                    node.InnerXml,
                    @"^(?i)(.+(?<!>\w{1,3})[\.\?])\s*([^\.\d]+?)\s*(\d+)\s*:\s*(\d+)\s*[-–]\s*(\d+)\s*\.?\s*$",
                    "<article-title>$1</article-title> <source>$2</source> <volume>$3</volume>: <fpage>$4</fpage>–<lpage>$5</lpage>.");
                /*
                 * Reference journal without volume and issue, only pages
                 * /<fake_tag>(.*?\.)\s*(.*?\.)\s*:\s*(\d+)\s*[-–](\d+)(\s*\.?\s*)<\/fake_tag>/umis
                 * <article-title>$1</article-title> <source>$2</source>: <fpage>$4</fpage>-<lpage>$5</lpage>$6
                 */
                node.InnerXml = Regex.Replace(
                    node.InnerXml,
                    @"^(?i)(.+(?<!>\w{1,3})[\.\?])\s*(.+?)\s*[\.,:]\s*(\d+)\s*[-–]\s*(\d+)\s*\.?\s*$",
                    "<article-title>$1</article-title> <source>$2</source>, <fpage>$3</fpage>–<lpage>$4</lpage>.");
                /*
                 * Monograph
                 * /<fake_tag>(.*?\.)\s*([^\d]+),\s*([^\d]+),\s*(\d+)\s*pp\.\s*<\/fake_tag>/umis
                 * <source>$1</source> <publisher-name>$2</publisher-name>, <publisher-loc>$3</publisher-loc>, <lpage>$4</lpage> pp.
                 */
                node.InnerXml = Regex.Replace(
                    node.InnerXml,
                    @"^(?i)(.*?\.)\s*([^\d]+),\s*([^\d]+),\s*(\d+)\s*pp\.\s*$",
                    "<source>$1</source> <publisher-name>$2</publisher-name>, <publisher-loc>$3</publisher-loc>, <size units=\"page\">$4 pp</size>.");
            }

            reference.InnerXml = Regex.Replace(reference.InnerXml, "</?fake_tag>", string.Empty);

            result = reference.InnerXml;

            return result;
        }

        private string ReferencePersonGroupSplit(string reference)
        {
            string result = reference;

            // Reference person group split
            /*
             * Article references person-group match
             * /(?<=\A|,)\s*(((van|von|Van|Von|fon|Fon|de|De|Le|Jr|Jr\.|jr|jr\.|den|Den)\s)*(\S+))\s(.*?)\s*(?=,|\Z)/musi
             * <name><surname>$1</surname> <given-names>$5</given-names></name>
             */
            /*
            if (node["person-group"] != null)
            {
                node["person-group"].InnerXml = Regex.Replace(node["person-group"].InnerXml,
                    @"\s*\bet\b\W*\bal\b\.?",
                    "<etal> et al.</etal>");
                node["person-group"].InnerXml = Regex.Replace(node["person-group"].InnerXml,
                    @"\s*([\w\s\.’‘'-]{2,50})\s+([\w\s\.-]{1,10})\s*",
                    "<name><surname>$1</surname> <given-names>$2</given-names></name>");
            }
            */

            // Reference person group split2
            /*
             * Article references person-group match
             * /(?<=\A|,)\s*(((van|von|Van|Von|fon|Fon|de|De|Le|Jr|Jr\.|jr|jr\.|den|Den)\s)*(\S+))\s(.*?)\s*(?=,|\Z)/musi
             * <name><surname>$1</surname> <given-names>$5</given-names></name>
             */
            /*
             *  foreach person-group:
             *      replace "(<person-group>[^<>]+,)\s*" by "$1<name name-style="western">"
             *      replace "(<person-group>)(?!<)\s*" by "$1<name name-style="western">"
             *      replace "(?<!>),<name" by "</name>,<name"
             *      replace "(?<!>)</person-group>" by "</name></person-group>"
             */

            return reference;
        }
    }
}
