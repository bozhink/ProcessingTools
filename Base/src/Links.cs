using System;
using System.Xml;
using System.Text.RegularExpressions;

namespace Base
{
    namespace Nlm
    {
        public class Links : Base
        {
            public Links()
                : base()
            {
            }

            public Links(string xml)
                : base(xml)
            {
            }

            public void TagWWW()
            {
                ParseXmlStringToXmlDocument();
                try
                {
                    XmlNodeList nodeList = xmlDocument.SelectNodes("//p|//license-p|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation", namespaceManager);
                    foreach (XmlNode node in nodeList)
                    {
                        //string replace = mp.Value;
                        //replace = Regex.Replace(replace, "(([a-z0-9_]*[\\.:@\\-])*([a-z0-9_]+)\\.(com|net|org|info|eu|uk|us|cn|gov|edu|ar|br)(:\\d+)?(/)?([^<>\n\"\\s]*[A-Za-z0-9/])?)", "<ext-link ext-link-type=\"uri\" xlink:href=\"http://$1\">$1</ext-link>");
                        //replace = Regex.Replace(replace, "((http(s?)|(s?)ftp)://)<ext\\-link [^>]*>([^<]*)</ext\\-link>", "$1$5");
                        //replace = Regex.Replace(replace, "(http(s?)://([a-z0-9_]*[\\.:@\\-])*([a-z0-9_]+)\\.([a-z]{2,4})(:\\d+)?(/[^<>\n\"\\s]*[A-Za-z0-9/])?)", "<ext-link ext-link-type=\"uri\" xlink:href=\"$1\">$1</ext-link>");
                        //replace = Regex.Replace(replace, "((s?)ftp://([a-z0-9_]*[\\.:@\\-])*([a-z0-9_]+)\\.([a-z]{2,4})(:\\d+)?(/[^<>\n\"\\s]*[A-Za-z0-9/])?)", "<ext-link ext-link-type=\"uri\" xlink:href=\"$1\">$1</ext-link>");
                        //replace = Regex.Replace(replace, @"(?<!href=\W)(((http(s)?:\/\/)|(www\.))([\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?))", "<ext-link ext-link-type=\"uri\" xlink:href=\"http$4://$5$6\">$1</ext-link>");
                        node.InnerXml = Regex.Replace(node.InnerXml,
                            @"(?<!<[^>]+)(((http(s)?:\/\/)|(www\.))([\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?))",
                            "<ext-link ext-link-type=\"uri\" xlink:href=\"http$4://$5$6\">$1</ext-link>");
                    }
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, this.GetType().Name, 1);
                }
                xml = xmlDocument.OuterXml;

                // Tag IP addresses
                xml = Regex.Replace(xml, "(?<!<[^>]+)(http(s?)://((\\d{1,3}\\.){3,3}\\d{1,3})(:\\d+)?(/[^<>\n\"\\s]*[A-Za-z0-9/])?)", "<ext-link ext-link-type=\"uri\" xlink:href=\"$1\">$1</ext-link>");
                xml = Regex.Replace(xml, "(?<!<[^>]+)((s?)ftp://((\\d{1,3}\\.){3,3}\\d{1,3})(:\\d+)?(/[^<>\n\"\\s]*[A-Za-z0-9/])?)", "<ext-link ext-link-type=\"uri\" xlink:href=\"$1\">$1</ext-link>");

                //int maxTagInTag = 10;
                //for (int i = 0; i < maxTagInTag; i++)
                //{
                //    ClearTagsFromTags();
                //}

                for (Match email = Regex.Match(xml, @"<email[^>]*>.*?</email>"); email.Success; email = email.NextMatch())
                {
                    string replace = Regex.Replace(email.Value, @"</?[^>]*>", "");
                    xml = Regex.Replace(xml, Regex.Escape(email.Value), "<email xlink:type=\"simple\">" + replace + "</email>");
                }
                for (Match email = Regex.Match(xml, @"<email[^>]*>.*?</email>"); email.Success; email = email.NextMatch())
                {
                    string replace = Regex.Replace(email.Value, @"(<email[^>]*>)\s*", "$1");
                    replace = Regex.Replace(replace, @"\s*(</email>)", "$1");
                    replace = Regex.Replace(replace, @"(\w)(\W)\s+?(\w)", "$1</email>$2 <email xlink:type=\"simple\">$3");
                    xml = Regex.Replace(xml, Regex.Escape(email.Value), replace);
                }
            }

            public void ClearTagsFromTags()
            {
                xml = Regex.Replace(xml, "(<[^<>]*)(<[^>]*>)([^<>]*)(</[^>]*>)([^>]*>)", "$1$3$5");
            }

            public void TagDOI()
            {
                // Remove blanks around brackets spanning numbers
                xml = Regex.Replace(xml, @"(\d)\s(\(|\[)([A-Z0-9]+)(\]|\))\s(\d)", "$1$2$3$4$5");
                // Tag DOI
                //xml = Regex.Replace(xml, @"doi:(\s*)([^,<\s]*[A-Za-z0-9])", "doi: <ext-link ext-link-type=\"uri\" xlink:href=\"http://dx.doi.org/$2\">$2</ext-link>");
                xml = Regex.Replace(xml, @"(?<!<ext-link [^>]*>)(\b[Dd][Oo][Ii]\b:?)\s*(\d+\.[^,<>\s]+[A-Za-z0-9]((?<=&[A-Za-z0-9#]+);)?)", "$1 <ext-link ext-link-type=\"doi\" xlink:href=\"$2\">$2</ext-link>");
                // Some format
                xml = Regex.Replace(xml, "(</source>)((?i)doi:?)", "$1 $2");
            }

            public void TagPMCLinks()
            {
                // PMid
                xml = Regex.Replace(xml, @"(?i)(?<=\bpmid\W?)(\d+)", "<ext-link ext-link-type=\"pmid\" xlink:href=\"$1\">$1</ext-link>");
                // PMCid
                xml = Regex.Replace(xml, @"(?i)(pmc\W?(\d+))", "<ext-link ext-link-type=\"pmcid\" xlink:href=\"PMC$2\">$1</ext-link>");
                xml = Regex.Replace(xml, @"(?i)(?<=\bpmcid\W?)(\d+)", "<ext-link ext-link-type=\"pmcid\" xlink:href=\"PMC$1\">$1</ext-link>");
            }
        }
    }

    namespace NlmSystem
    {
        public class Links : Base
        {
            public Links()
            {
                this.xml = "";
            }

            public Links(string xml)
            {
                this.xml = xml;
            }

            public void TagWWW()
            {
                Match mp = Regex.Match(xml, "(<p>[\\s\\S]*?</p>)");
                while (mp.Success)
                {
                    string replace = mp.Value;
                    replace = Regex.Replace(replace, @"(((http(s)?:\/\/)|(www\.))([\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?))",
                        "<a target=\"_blank\" href=\"http$4://$5$6\">$1</a>");
                    xml = Regex.Replace(xml, Regex.Escape(mp.Value), replace);
                    mp = mp.NextMatch();
                }
                // Tag IP addresses
                xml = Regex.Replace(xml, "(http(s?)://((\\d{1,3}\\.){3,3}\\d{1,3})(:\\d+)?(/[^<>\n\"\\s]*[A-Za-z0-9/])?)", "<a target=\"_blank\" href=\"$1\">$1</a>");
                xml = Regex.Replace(xml, "((s?)ftp://((\\d{1,3}\\.){3,3}\\d{1,3})(:\\d+)?(/[^<>\n\"\\s]*[A-Za-z0-9/])?)", "<a target=\"_blank\" href=\"$1\">$1</a>");

                int maxTagInTag = 10;
                for (int i = 0; i < maxTagInTag; i++)
                {
                    ClearTagsFromTags();
                }
            }

            public void ClearTagsFromTags()
            {
                xml = Regex.Replace(xml, "(<[^<>]*)(<[^>]*>)([^<>]*)(</[^>]*>)([^>]*>)", "$1$3$5");
            }

            public void TagDOI()
            {
                // Remove blanks around brackets spanning numbers
                xml = Regex.Replace(xml, @"(\d)\s(\(|\[)([A-Z0-9]+)(\]|\))\s(\d)", "$1$2$3$4$5");
                // Tag DOI
                xml = Regex.Replace(xml, @"doi:(\s*)([^,<\s]*[A-Za-z0-9])", "doi: <a target=\"_blank\" href=\"http://dx.doi.org/$2\">$2</a>");
            }
        }
    }
}
