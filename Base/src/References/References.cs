using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace ProcessingTools.Base
{
    public class References : TaggerBase
    {
        public References(Config config, string xml)
            : base(config, xml)
        {
        }

        public References(TaggerBase baseObject)
            : base(baseObject)
        {
        }

        public static string ReferencePartSplitter(XmlNode reference)
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

        public static string ReferenceJournalMatch(XmlNode reference)
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

        public static string ReferencePersonGroupSplit(string reference)
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

        public void GenerateTagTemplateXml()
        {
            FileProcessor fp = new FileProcessor(this.Config);
            ////this.Xml = Regex.Replace(this.Xml, "<!DOCTYPE [^>]*>", string.Empty);
            {
                // References list
                fp.OutputFileName = this.Config.referencesGetReferencesXmlPath;
                fp.Xml = this.XmlDocument.ApplyXslTransform(this.Config.referencesGetReferencesXslPath);
                fp.Write();
            }

            {
                // References template
                fp.OutputFileName = this.Config.referencesTagTemplateXmlPath;
                fp.Xml = this.XmlDocument.ApplyXslTransform(this.Config.referencesTagTemplateXslPath);
                fp.Xml = fp.XmlDocument.ApplyXslTransform(this.Config.referencesSortReferencesXslPath);
                fp.Write();
            }
        }

        public void TagReferences()
        {
            string xml = this.Xml;

            /*
             * Tag references using generated template
             */
            XmlDocument xd = new XmlDocument();
            try
            {
                xd.Load(this.Config.referencesTagTemplateXmlPath);
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 1);
            }

            XmlNode referenceList = xd.DocumentElement.LastChild;
            for (int i = referenceList.ChildNodes.Count - 1; i >= 0; i--)
            {
                XmlNode reference = referenceList.ChildNodes.Item(i);

                string id = reference.Attributes["id"].Value;
                string year = Regex.Escape(reference.Attributes["year"].Value);
                string authors = Regex.Replace(reference.Attributes["authors"].Value, @"\W+", "\\W*");
                reference.Attributes["authors"].Value = authors;

                xml = Regex.Replace(xml, "(?i)(?<!<xref [^>]*>)(?<!<[^>]+)(" + authors + "[’´'s]*\\s*\\(" + year + "\\))", "<xref ref-type=\"bibr\" rid=\"" + id + "\">$1</xref>");
                xml = Regex.Replace(xml, "(?i)(?<!<xref [^>]*>)(?<!<[^>]+)(" + authors + "[’´'s]*\\s*\\[" + year + "\\])", "<xref ref-type=\"bibr\" rid=\"" + id + "\">$1</xref>");
                xml = Regex.Replace(xml, "(?i)(?<!<xref [^>]*>)(?<!<[^>]+)(" + authors + "[’´'s]*\\s*[\\(\\[]" + year + ")(?=[;:,\\s])", "<xref ref-type=\"bibr\" rid=\"" + id + "\">$1</xref>");
                xml = Regex.Replace(xml, "(?i)(?<!<xref [^>]*>)(?<!<[^>]+)(" + authors + "[’´'s]*\\s*\\[[a-z\\d\\W]{0,20}\\]\\s*)(" + year + ")", "$1<xref ref-type=\"bibr\" rid=\"" + id + "\">$2</xref>");
                xml = Regex.Replace(xml, "(?i)(?<!<xref [^>]*>)(?<!<[^>]+)(" + authors + "[’´'s]*\\s*" + year + ")", "<xref ref-type=\"bibr\" rid=\"" + id + "\">$1</xref>");
            }

            // Polenec 1952, 1954, 1957, 1958, 1959, 1960, 1961a, b, c, d, 1962a
            for (int i = 0; i < 10; i++)
            {
                for (Match m = Regex.Match(xml, @"<xref ref-type=""bibr""[^>]+>[^<>]+(?<=[^\)\]])</xref>(\W\s*\b(\d{2,4}(\W\d{1,4})?[a-z]?|[a-z])\b)*\W\s*\b\d{2,4}(\W\d{1,4})?[a-z]?\b"); m.Success; m = m.NextMatch())
                {
                    string rid = Regex.Replace(m.Value, @"\A.*<xref [^<>]*rid=""(\w*?)""[^<>]*>.*\Z", "$1");
                    string authors = referenceList.SelectSingleNode("//reference[@id='" + rid + "']/@authors", NamespaceManager).InnerText;

                    string replace = m.Value;

                    for (Match l = Regex.Match(m.Value, @"(?<=<xref [^>]+>[^<>]*</xref>(\W\s*\b(\d{2,4}(\W\d{1,4})?[a-z]?|[a-z])\b)*\W\s*)\b\d{2,4}(\W\d{1,4})?[a-z]?\b"); l.Success; l = l.NextMatch())
                    {
                        try
                        {
                            XmlNode node = referenceList.SelectSingleNode("//reference[@authors='" + authors + "'][@year='" + l.Value + "']", NamespaceManager);
                            if (node != null)
                            {
                                replace = Regex.Replace(
                                    replace,
                                    "(?<=<xref [^>]+>[^<>]*</xref>(\\W\\s*\\b(\\d{2,4}(\\W\\d{1,4})?[a-z]?|[a-z])\\b)*\\W\\s*)\\b" + l.Value + "\\b",
                                    "<xref ref-type=\"bibr\" rid=\"" + node.Attributes["id"].Value + "\">" + l.Value + "</xref>");
                            }
                        }
                        catch (Exception e)
                        {
                            Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
                        }
                    }

                    xml = Regex.Replace(xml, Regex.Escape(m.Value), replace);
                }
            }

            // <xref ref-type="bibr" rid="B38">Kitahara et al. 2012a</xref>, b, c
            for (Match m = Regex.Match(xml, @"<xref ref-type=""bibr"" [^>]+>[^<>]*\d+\W*[a-z]\W*</xref>(\W\s*(\b[a-z]\b))+"); m.Success; m = m.NextMatch())
            {
                string rid = Regex.Replace(m.Value, @"\A.*<xref [^<>]*rid=""(\w+)""[^<>]*>.*\Z", "$1");
                string authors = referenceList.SelectSingleNode("//reference[@id='" + rid + "']/@authors", NamespaceManager).InnerText;
                string year = Regex.Replace(referenceList.SelectSingleNode("//reference[@id='" + rid + "']/@year", NamespaceManager).InnerText, "[A-Za-z]", string.Empty);

                string replace = m.Value;

                for (Match l = Regex.Match(m.Value, @"(?<=<xref [^>]+>[^<>]*</xref>(\W\s*\b[a-z]\b)*\W\s*)\b[a-z]\b"); l.Success; l = l.NextMatch())
                {
                    try
                    {
                        XmlNode node = referenceList.SelectSingleNode("//reference[@authors='" + authors + "'][@year='" + year + l.Value + "']", NamespaceManager);
                        if (node != null)
                        {
                            replace = Regex.Replace(
                                replace,
                                "(?<=<xref [^>]+>[^<>]*</xref>(\\W\\s*\\b[a-z]\\b)*\\W\\s*)\\b" + l.Value + "\\b",
                                "<xref ref-type=\"bibr\" rid=\"" + node.Attributes["id"].Value + "\">" + l.Value + "</xref>");
                        }
                    }
                    catch (Exception e)
                    {
                        Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
                    }
                }

                xml = Regex.Replace(xml, Regex.Escape(m.Value), replace);
            }

            // This loop must be executed separately because is slow
            for (int i = referenceList.ChildNodes.Count - 1; i >= 0; i--)
            {
                XmlNode reference = referenceList.ChildNodes.Item(i);

                string id = reference.Attributes["id"].Value;
                string year = Regex.Escape(reference.Attributes["year"].Value);
                string authors = Regex.Replace(reference.Attributes["authors"].Value, @"\W+", "\\W*");
                reference.Attributes["authors"].Value = authors;

                xml = Regex.Replace(xml, "(?i)(?<!<xref [^>]*>)(?<!<[^>]+)(" + authors + "[’'s]*\\s*[\\(\\[]*(\\d{4,4}[a-z]?[,;\\s–-]*(and|&amp;|[a-z])?\\s*)+)(" + year + ")", "$1<xref ref-type=\"bibr\" rid=\"" + id + "\">$4</xref>");
                ////xml = Regex.Replace(xml, "(?i)(?<!<xref [^>]*>)(?<!<[^>]+)(" + authors + "[’'s]*\\s*[\\(\\[]*(\\d{4,4}[a-z]?[,;\\s–-]*(and|&amp;|[a-z])?\\s*)+([a-z][,;\\s–-]*(and|&amp;|[a-z])?\\s*)*)(" + year + ")", "$1<xref ref-type=\"bibr\" rid=\"" + id + "\">$6</xref>");
            }
            //// TODO: Call here the two loops above

            this.Xml = xml;
        }

        public void SplitReferences()
        {
            try
            {
                XmlNodeList nodeList = this.XmlDocument.SelectNodes("//element-citation|//mixed-citation|nlm-citation", this.NamespaceManager);
                foreach (XmlNode node in nodeList)
                {
                    node.InnerXml = ReferencePartSplitter(node);
                    node.InnerXml = ReferenceJournalMatch(node);
                    node.InnerXml = ReferencePersonGroupSplit(node.InnerXml);
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }
        }
    }
}
