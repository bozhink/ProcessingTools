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

        protected IEnumerable<string> ClearFakeTaxaNames(IEnumerable<string> taxaNames)
        {
            IEnumerable<string> result = taxaNames;
            try
            {
                // Clear taxa-like person names in taxa names list
                result = this.ClearTaxaLikePersonNamesInArticle(result);

                // Apply blacklist
                result = result.DistinctWithStringList(this.BlackList.StringList, true, false);
            }
            catch
            {
                throw;
            }

            return result;
        }

        private IEnumerable<string> ClearTaxaLikePersonNamesInArticle(IEnumerable<string> taxaNames)
        {
            // Get taxa-like person name parts
            IEnumerable<string> taxaLikePersonNameParts = this.XmlDocument
                .SelectNodes("//surname[string-length(normalize-space(.)) > 2]|//given-names[string-length(normalize-space(.)) > 2]")
                .Cast<XmlNode>()
                .Select(s => s.InnerText)
                .Distinct()
                .MatchWithStringList(
                    taxaNames.GetFirstWord(),
                    true,
                    true);

            return taxaNames.DistinctWithStringList(taxaLikePersonNameParts.Select(Regex.Escape), true, false);
        }

        protected IEnumerable<string> GetTaxaItemsByWhiteList()
        {
            string textToMine = String.Join(" ", this.TextWords);
            IEnumerable<string> whiteListedItems = textToMine.MatchWithStringList(this.WhiteList.StringList, true, false);

            return whiteListedItems;
        }


        public abstract void Tag();

        public abstract void Tag(IXPathProvider xpathProvider);
    }
}