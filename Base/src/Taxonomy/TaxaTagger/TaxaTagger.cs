namespace ProcessingTools.Base.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using System.Xml.Linq;

    public abstract class TaxaTagger : TaggerBase, ITagger
    {
        protected const string HigherTaxaReplacePattern = "<tn type=\"higher\">$1</tn>";
        protected const string LowerRaxaReplacePattern = "<tn type=\"lower\">$1</tn>";

        private IStringDataList whiteList;
        private IStringDataList blackList;

        public TaxaTagger(string xml, IStringDataList whiteList, IStringDataList blackList)
            : base(xml)
        {
            this.WhiteList = whiteList;
            this.BlackList = blackList;
        }

        public TaxaTagger(Config config, string xml, IStringDataList whiteList, IStringDataList blackList)
            : base(config, xml)
        {
            this.WhiteList = whiteList;
            this.BlackList = blackList;
        }

        public TaxaTagger(IBase baseObject, IStringDataList whiteList, IStringDataList blackList)
            : base(baseObject)
        {
            this.WhiteList = whiteList;
            this.BlackList = blackList;
        }

        protected IStringDataList WhiteList
        {
            get
            {
                return this.whiteList;
            }

            private set
            {
                this.whiteList = value;
            }
        }

        protected IStringDataList BlackList
        {
            get
            {
                return this.blackList;
            }

            private set
            {
                this.blackList = value;
            }
        }

        public void UntagTaxa()
        {
            try
            {
                this.RemoveFalseTaxaOfPersonNames();

                this.ApplyBlackList();

                this.XmlDocument.InnerXml = Regex.Replace(this.XmlDocument.InnerXml, @"<tn type=""higher"">([a-z]+)</tn>", "$1");
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }
        }

        private void RemoveFalseTaxaOfPersonNames()
        {
            try
            {
                IEnumerable<string> personNameParts = this.XmlDocument.SelectNodes("//surname[string-length(normalize-space(.)) > 2]|//given-names[string-length(normalize-space(.)) > 2]")
                    .Cast<XmlNode>().Select(s => s.InnerText).Distinct();

                IEnumerable<string> firstWordTaxaList = this.XmlDocument.GetFirstWordOfTaxaNames();
                foreach (string taxon in firstWordTaxaList)
                {
                    if (taxon.IndexOf('.') < 0)
                    {
                        Regex matchTaxonInName = new Regex("(?i)\\b" + Regex.Escape(taxon) + "\\b");
                        IEnumerable<string> queryResult = from item in personNameParts
                                                          where matchTaxonInName.Match(item).Success
                                                          select matchTaxonInName.Match(item).Value;

                        foreach (string item in queryResult)
                        {
                            if (item.IndexOf('.') < 0)
                            {
                                this.XmlDocument.InnerXml = Regex.Replace(this.XmlDocument.InnerXml, "<tn [^>]*>((?i)" + item + "(\\s+.*?)?(\\.?))</tn>", "$1");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }
        }

        protected XElement GetWhiteList()
        {
            return XElement.Load(this.Config.whiteListXmlFilePath);
        }

        protected void ApplyWhiteList()
        {
            try
            {
                IEnumerable<string> whiteListItems = this.WhiteList.StringList;
                foreach (string item in whiteListItems)
                {
                    this.XmlDocument.InnerXml = Regex.Replace(
                        this.XmlDocument.InnerXml,
                        "(?<!<tn [^>]*>)(?<!name [^>]*>)(?<!<[^>]+=\"[^>]*)(?i)\\b(" + item + ")\\b(?!\"\\s?>)(?!</tn)(?!</tp:)(?!</named-content></kwd>)",
                        HigherTaxaReplacePattern);
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Applying white list.");
            }
        }

        protected XElement GetBlackList()
        {
            return XElement.Load(this.Config.blackListXmlFilePath);
        }

        protected void ApplyBlackList()
        {
            try
            {
                string xml = this.XmlDocument.InnerXml;

                IEnumerable<string> wrongTaxaNames = this.XmlDocument
                    .GetFirstWordOfTaxaNames()
                    .MatchWithStringList(this.BlackList.StringList, true);

                foreach (string wrongTaxon in wrongTaxaNames)
                {
                    xml = Regex.Replace(xml, "<tn [^>]*>((?i)" + wrongTaxon + "(\\s+.*?)?(\\.?))</tn>", "$1");
                }

                this.XmlDocument.InnerXml = xml;
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Apply black list.");
            }
        }

        public abstract void Tag();

        public abstract void Tag(IXPathProvider xpathProvider);

        ////// Flora-like tagging methods
        ////public void PerformFloraReplace(string xmlTemplate)
        ////{
        ////    XmlDocument template = new XmlDocument();
        ////    template.LoadXml(xmlTemplate);

        ////    XmlNode root = template.DocumentElement;
        ////    Alert.Log(root.ChildNodes.Count);

        ////    for (int i = root.ChildNodes.Count - 1; i >= 0; i--)
        ////    {
        ////        XmlNode taxon = root.ChildNodes.Item(i);
        ////        XmlNode find = taxon.FirstChild;
        ////        XmlNode replace = taxon.LastChild;

        ////        Alert.Log(find.InnerXml);

        ////        string pattern = find.InnerXml;
        ////        pattern = Regex.Replace(pattern, @"\.", "\\.");
        ////        pattern = Regex.Replace(pattern, @"(?<=\w)\s+(?=\w)", @"\b\s*\b");
        ////        pattern = Regex.Replace(pattern, @"(?<=\W)\s+(?=\w)", @"?\s*\b");
        ////        pattern = Regex.Replace(pattern, @"(?<=\W)\s+", @"?\s*");
        ////        pattern = Regex.Replace(pattern, @"\bvar\b", "(var|v)");

        ////        pattern = "(?i)" + pattern;

        ////        this.Xml = Regex.Replace(
        ////            this.Xml,
        ////            "(?<![a-z-])(?<!<[^>]+=\"[^>]*)(?<!<tn>)(" + pattern + ")(?![A-Za-z])(?!</tn\\W)(?!</tp:)(?!</name>)",
        ////            "<tn>$1</tn>");
        ////    }

        ////    ////string infraspecificPattern = "\\b([Vv]ar\\.|[Ss]ubsp\\.|([Ss]ub)?[Ss]ect\\.|[Aa]ff\\.|[Cc]f\\.|[Ff]orma)";
        ////    ////string lowerPattern = "\\s*\\b[a-z]*(ensis|ulei|onis|oidis|oide?a|phyll[au][sm]?|[aeiou]lii|longiflora)\\b";

        ////    ////xml = Regex.Replace(xml, infraspecificPattern + "\\s*<tn>", "<tn>$1 ");
        ////    ////xml = Regex.Replace(xml, "(?<!<tn>)(" + infraspecificPattern + "\\s+[A-Z]?[a-z\\.-]+)(?!</tn>)", "<tn>$1</tn>");

        ////    ////xml = Regex.Replace(xml, @"<tn>([A-Z][a-z\.-]+)</tn>\s+<tn>([a-z\.-]+)</tn>", "<tn>$1 $2</tn>");
        ////    ////xml = Regex.Replace(xml, "(<tn>)" + infraspecificPattern + "</tn>\\s+<tn>", "$1$2 ");

        ////    ////xml = Regex.Replace(xml, "</tn>\\s*<tn>" + infraspecificPattern, " $1");

        ////    ////// TODO: Here we must remove tn/tn
        ////    ////{
        ////    ////    ParseXmlStringToXmlDocument();
        ////    ////    XmlNodeList nodeList = xmlDocument.SelectNodes("//tn[name(..)!='tn'][count(.//tn)!=0]");
        ////    ////    foreach (XmlNode node in nodeList)
        ////    ////    {
        ////    ////        node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", "");
        ////    ////    }
        ////    ////    xml = xmlDocument.OuterXml;
        ////    ////}

        ////    ////// Guess new taxa:
        ////    ////for (int i = 0; i < 10; i++)
        ////    ////{
        ////    ////    xml = Regex.Replace(xml,
        ////    ////        "(</tn>,?(\\s+and)?\\s+)(" + infraspecificPattern + "?" + lowerPattern + ")",
        ////    ////        "$1<tn>$3</tn>");
        ////    ////}

        ////    //// Genus <tn>species</tn>. The result will be <tn>Genus <tn>species</tn></tn>
        ////    ////xml = Regex.Replace(xml, @"([^\.\s]\s+)([A-Z][a-z\.-]+\s+<tn>[a-z\.-]+.*?</tn>)", "$1<tn>$2</tn>");

        ////    ////xml = Regex.Replace(xml, "\\b([A-Z][a-z\\.-]+(\\s*[a-z\\.-]+)?\\s+<tn>" + infraspecificPattern + "\\s*[a-z\\.-]+.*?</tn>)", "<tn>$1</tn>");

        ////    ////xml = Regex.Replace(xml,
        ////    ////    "(([A-Z][a-z\\.-]+|<tn>.*?</tn>)\\s+([a-z\\.-]*\\s*" + infraspecificPattern + ")?" + lowerPattern + ")",
        ////    ////    "<tn>$1</tn>");

        ////    ////// TODO: Here we must remove tn/tn
        ////    ////{
        ////    ////    ParseXmlStringToXmlDocument();
        ////    ////    XmlNodeList nodeList = xmlDocument.SelectNodes("//tn[name(..)!='tn'][count(.//tn)!=0]");
        ////    ////    foreach (XmlNode node in nodeList)
        ////    ////    {
        ////    ////        node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", "");
        ////    ////    }
        ////    ////    xml = xmlDocument.OuterXml;
        ////    ////}

        ////    ////// Remove taxa in toTaxon
        ////    ////{
        ////    ////    ParseXmlStringToXmlDocument();
        ////    ////    XmlNodeList nodeList = xmlDocument.SelectNodes("//toTaxon[count(.//tn)!=0]");
        ////    ////    foreach (XmlNode node in nodeList)
        ////    ////    {
        ////    ////        node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", "");
        ////    ////    }
        ////    ////    xml = xmlDocument.OuterXml;
        ////    ////}
        ////}
    }
}