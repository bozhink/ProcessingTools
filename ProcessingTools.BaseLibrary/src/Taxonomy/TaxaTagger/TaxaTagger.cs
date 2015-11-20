namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Configurator;
    using Contracts;
    using Extensions;

    public abstract class TaxaTagger : TaggerBase, IBaseTagger
    {
        protected const string HigherTaxaReplacePattern = "<tn type=\"higher\">$1</tn>";
        protected const string LowerRaxaReplacePattern = "<tn type=\"lower\">$1</tn>";

        private IStringDataList blackList;
        private IStringDataList whiteList;

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

        public abstract void Tag();

        protected IEnumerable<string> ClearFakeTaxaNames(IEnumerable<string> taxaNames)
        {
            var result = taxaNames;

            try
            {
                var taxaNamesFirstWord = new HashSet<string>(result
                        .GetFirstWord()
                        .Select(Regex.Escape));

                result = this.ClearFakeTaxaNamesLikePersonNamesInArticle(result, taxaNamesFirstWord);
                result = this.ClearFakeTaxaNamesUsingBlackList(result, taxaNamesFirstWord);
            }
            catch
            {
                throw;
            }

            return new HashSet<string>(result);
        }

        protected IEnumerable<string> GetTaxaItemsByWhiteList()
        {
            string textToMine = string.Join(" ", this.TextWords);
            IEnumerable<string> result = textToMine.MatchWithStringList(this.WhiteList.StringList, false, false, false);

            return new HashSet<string>(result);
        }

        private IEnumerable<string> ClearFakeTaxaNamesUsingBlackList(IEnumerable<string> taxaNames, HashSet<string> taxaNamesFirstWord)
        {
            var blackListedNames = new HashSet<string>(taxaNamesFirstWord
                .MatchWithStringList(this.BlackList.StringList, true, false, true));

            var result = taxaNames
                .Where(name => !blackListedNames.Contains(name.GetFirstWord()));

            return new HashSet<string>(result);
        }

        private IEnumerable<string> ClearFakeTaxaNamesLikePersonNamesInArticle(IEnumerable<string> taxaNames, IEnumerable<string> taxaNamesFirstWord)
        {
            var taxaLikePersonNameParts = new HashSet<string>(this.XmlDocument
                .SelectNodes("//surname[string-length(normalize-space(.)) > 2]|//given-names[string-length(normalize-space(.)) > 2]")
                .Cast<XmlNode>()
                .Select(s => s.InnerText)
                .MatchWithStringList(taxaNamesFirstWord, false, true, true)
                .Select(Regex.Escape));

            var result = new HashSet<string>(taxaNames
                .DistinctWithStringList(taxaLikePersonNameParts, true, false, true));

            return result;
        }
    }
}