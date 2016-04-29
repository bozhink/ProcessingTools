namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider;
    using ProcessingTools.Extensions;

    public abstract class TaxaTagger : TaxPubDocument, ITagger
    {
        private ITaxonomicBlackListDataService service;

        public TaxaTagger(string xml, ITaxonomicBlackListDataService service)
            : base(xml)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        public abstract Task Tag();

        protected async Task<IEnumerable<string>> ClearFakeTaxaNames(IEnumerable<string> taxaNames)
        {
            var result = taxaNames;

            try
            {
                var taxaNamesFirstWord = new HashSet<string>(result
                    .GetFirstWord()
                    .Select(Regex.Escape));

                result = this.ClearFakeTaxaNamesLikePersonNamesInArticle(result, taxaNamesFirstWord);
                result = await this.ClearFakeTaxaNamesUsingBlackList(result, taxaNamesFirstWord);
            }
            catch
            {
                throw;
            }

            return new HashSet<string>(result);
        }

        private async Task<IEnumerable<string>> ClearFakeTaxaNamesUsingBlackList(IEnumerable<string> taxaNames, HashSet<string> taxaNamesFirstWord)
        {
            var blackListItems = new HashSet<string>(await this.service.All());

            var blackListedNames = new HashSet<string>(taxaNamesFirstWord
                .MatchWithStringList(blackListItems, true, false, true));

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