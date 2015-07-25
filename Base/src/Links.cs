using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace ProcessingTools.Base
{
    namespace Nlm
    {
        public class LinksTagger : Base
        {
            public LinksTagger()
                : base()
            {
            }

            public LinksTagger(string xml)
                : base(xml)
            {
            }

            public LinksTagger(Base baseObject)
                : base(baseObject)
            {
            }

            public void TagWWW()
            {
                this.ParseXmlStringToXmlDocument();
                try
                {
                    XmlNodeList nodeList = this.xmlDocument.SelectNodes("//p|//license-p|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation", this.NamespaceManager);
                    foreach (XmlNode node in nodeList)
                    {
                        ////string replace = mp.Value;
                        ////replace = Regex.Replace(replace, "(([a-z0-9_]*[\\.:@\\-])*([a-z0-9_]+)\\.(com|net|org|info|eu|uk|us|cn|gov|edu|ar|br)(:\\d+)?(/)?([^<>\n\"\\s]*[A-Za-z0-9/])?)", "<ext-link ext-link-type=\"uri\" xlink:href=\"http://$1\">$1</ext-link>");
                        ////replace = Regex.Replace(replace, "((http(s?)|(s?)ftp)://)<ext\\-link [^>]*>([^<]*)</ext\\-link>", "$1$5");
                        ////replace = Regex.Replace(replace, "(http(s?)://([a-z0-9_]*[\\.:@\\-])*([a-z0-9_]+)\\.([a-z]{2,4})(:\\d+)?(/[^<>\n\"\\s]*[A-Za-z0-9/])?)", "<ext-link ext-link-type=\"uri\" xlink:href=\"$1\">$1</ext-link>");
                        ////replace = Regex.Replace(replace, "((s?)ftp://([a-z0-9_]*[\\.:@\\-])*([a-z0-9_]+)\\.([a-z]{2,4})(:\\d+)?(/[^<>\n\"\\s]*[A-Za-z0-9/])?)", "<ext-link ext-link-type=\"uri\" xlink:href=\"$1\">$1</ext-link>");
                        ////replace = Regex.Replace(replace, @"(?<!href=\W)(((http(s)?:\/\/)|(www\.))([\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?))", "<ext-link ext-link-type=\"uri\" xlink:href=\"http$4://$5$6\">$1</ext-link>");
                        node.InnerXml = Regex.Replace(
                            node.InnerXml,
                            @"(?<!<[^>]+)(((http(s)?:\/\/)|(www\.))([\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?))",
                            "<ext-link ext-link-type=\"uri\" xlink:href=\"http$4://$5$6\">$1</ext-link>");
                    }
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForMethod(e, this.GetType().Name, 1);
                }

                this.xml = this.xmlDocument.OuterXml;

                // Tag IP addresses
                this.xml = Regex.Replace(this.xml, "(?<!<[^>]+)(http(s?)://((\\d{1,3}\\.){3,3}\\d{1,3})(:\\d+)?(/[^<>\n\"\\s]*[A-Za-z0-9/])?)", "<ext-link ext-link-type=\"uri\" xlink:href=\"$1\">$1</ext-link>");
                this.xml = Regex.Replace(this.xml, "(?<!<[^>]+)((s?)ftp://((\\d{1,3}\\.){3,3}\\d{1,3})(:\\d+)?(/[^<>\n\"\\s]*[A-Za-z0-9/])?)", "<ext-link ext-link-type=\"uri\" xlink:href=\"$1\">$1</ext-link>");

                ////int maxTagInTag = 10;
                ////for (int i = 0; i < maxTagInTag; i++)
                ////{
                ////    ClearTagsFromTags();
                ////}

                for (Match email = Regex.Match(this.xml, @"<email[^>]*>.*?</email>"); email.Success; email = email.NextMatch())
                {
                    string replace = Regex.Replace(email.Value, @"</?[^>]*>", string.Empty);
                    this.xml = Regex.Replace(this.xml, Regex.Escape(email.Value), "<email xlink:type=\"simple\">" + replace + "</email>");
                }

                for (Match email = Regex.Match(this.xml, @"<email[^>]*>.*?</email>"); email.Success; email = email.NextMatch())
                {
                    string replace = Regex.Replace(email.Value, @"(<email[^>]*>)\s*", "$1");
                    replace = Regex.Replace(replace, @"\s*(</email>)", "$1");
                    replace = Regex.Replace(replace, @"(\w)(\W)\s+?(\w)", "$1</email>$2 <email xlink:type=\"simple\">$3");
                    this.xml = Regex.Replace(this.xml, Regex.Escape(email.Value), replace);
                }
            }

            public void ClearTagsFromTags()
            {
                this.xml = Regex.Replace(this.xml, "(<[^<>]*)(<[^>]*>)([^<>]*)(</[^>]*>)([^>]*>)", "$1$3$5");
            }

            public void TagDOI()
            {
                // Remove blanks around brackets spanning numbers
                this.xml = Regex.Replace(this.xml, @"(\d)\s(\(|\[)([A-Z0-9]+)(\]|\))\s(\d)", "$1$2$3$4$5");

                // Tag DOI
                ////xml = Regex.Replace(xml, @"doi:(\s*)([^,<\s]*[A-Za-z0-9])", "doi: <ext-link ext-link-type=\"uri\" xlink:href=\"http://dx.doi.org/$2\">$2</ext-link>");
                this.xml = Regex.Replace(this.xml, @"(?<!<ext-link [^>]*>)(\b[Dd][Oo][Ii]\b:?)\s*(\d+\.[^,<>\s]+[A-Za-z0-9]((?<=&[A-Za-z0-9#]+);)?)", "$1 <ext-link ext-link-type=\"doi\" xlink:href=\"$2\">$2</ext-link>");

                // Some format
                this.xml = Regex.Replace(this.xml, "(</source>)((?i)doi:?)", "$1 $2");
            }

            public void TagPMCLinks()
            {
                // PMid
                this.xml = Regex.Replace(this.xml, @"(?i)(?<=\bpmid\W?)(\d+)", "<ext-link ext-link-type=\"pmid\" xlink:href=\"$1\">$1</ext-link>");

                // PMCid
                this.xml = Regex.Replace(this.xml, @"(?i)(pmc\W?(\d+))", "<ext-link ext-link-type=\"pmcid\" xlink:href=\"PMC$2\">$1</ext-link>");

                this.xml = Regex.Replace(this.xml, @"(?i)(?<=\bpmcid\W?)(\d+)", "<ext-link ext-link-type=\"pmcid\" xlink:href=\"PMC$1\">$1</ext-link>");
            }
        }
    }

    namespace NlmSystem
    {
        public class LinksTagger : Base
        {
            public LinksTagger()
                : base()
            {
            }

            public LinksTagger(string xml)
                : base(xml)
            {
            }

            public LinksTagger(Base baseObject)
                : base(baseObject)
            {
            }

            public void TagWWW()
            {
                Match mp = Regex.Match(this.xml, "(<p>[\\s\\S]*?</p>)");
                while (mp.Success)
                {
                    string replace = mp.Value;
                    replace = Regex.Replace(
                        replace,
                        @"(((http(s)?:\/\/)|(www\.))([\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?))",
                        "<a target=\"_blank\" href=\"http$4://$5$6\">$1</a>");
                    this.xml = Regex.Replace(this.xml, Regex.Escape(mp.Value), replace);
                    mp = mp.NextMatch();
                }

                // Tag IP addresses
                this.xml = Regex.Replace(this.xml, "(http(s?)://((\\d{1,3}\\.){3,3}\\d{1,3})(:\\d+)?(/[^<>\n\"\\s]*[A-Za-z0-9/])?)", "<a target=\"_blank\" href=\"$1\">$1</a>");
                this.xml = Regex.Replace(this.xml, "((s?)ftp://((\\d{1,3}\\.){3,3}\\d{1,3})(:\\d+)?(/[^<>\n\"\\s]*[A-Za-z0-9/])?)", "<a target=\"_blank\" href=\"$1\">$1</a>");

                int maxTagInTag = 10;
                for (int i = 0; i < maxTagInTag; i++)
                {
                    this.ClearTagsFromTags();
                }
            }

            public void ClearTagsFromTags()
            {
                this.xml = Regex.Replace(this.xml, "(<[^<>]*)(<[^>]*>)([^<>]*)(</[^>]*>)([^>]*>)", "$1$3$5");
            }

            public void TagDOI()
            {
                // Remove blanks around brackets spanning numbers
                this.xml = Regex.Replace(this.xml, @"(\d)\s(\(|\[)([A-Z0-9]+)(\]|\))\s(\d)", "$1$2$3$4$5");

                // Tag DOI
                this.xml = Regex.Replace(this.xml, @"doi:(\s*)([^,<\s]*[A-Za-z0-9])", "doi: <a target=\"_blank\" href=\"http://dx.doi.org/$2\">$2</a>");
            }
        }
    }
}
