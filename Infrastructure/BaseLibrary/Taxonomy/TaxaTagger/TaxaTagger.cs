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
    using ProcessingTools.Strings.Extensions;

    public abstract class TaxaTagger : TaxPubDocument, ITagger
    {
        private readonly IBiotaxonomicBlackListIterableDataService service;

        public TaxaTagger(string xml, IBiotaxonomicBlackListIterableDataService service)
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
            var taxaNamesWithoutPersonNames = await this.ClearFakeTaxaNamesLikePersonNamesInArticle(taxaNames);
            var taxaNamesWithoutBlackListed = await this.ClearFakeTaxaNamesUsingBlackList(taxaNamesWithoutPersonNames);

            var result = new HashSet<string>(taxaNamesWithoutBlackListed);

            return result;
        }

        private Task<IEnumerable<string>> GetTaxaNamesFirstWords(IEnumerable<string> taxaNames) => Task.Run<IEnumerable<string>>(() =>
        {
            var words = taxaNames
                .GetFirstWord()
                .Select(Regex.Escape)
                .ToArray();

            return new HashSet<string>(words);
        });

        private async Task<IEnumerable<string>> ClearFakeTaxaNamesUsingBlackList(IEnumerable<string> taxaNames)
        {
            var taxaNamesFirstWord = await this.GetTaxaNamesFirstWords(taxaNames);
            var blackListItems = await this.service.All();

            var blackListedNames = taxaNamesFirstWord.MatchWithStringList(blackListItems, true, false, true);

            var result = taxaNames
                .Where(name => !blackListedNames.Contains(name.GetFirstWord()))
                .ToArray();

            return new HashSet<string>(result);
        }

        private async Task<IEnumerable<string>> ClearFakeTaxaNamesLikePersonNamesInArticle(IEnumerable<string> taxaNames)
        {
            var taxaNamesFirstWord = await this.GetTaxaNamesFirstWords(taxaNames);

            var taxaLikePersonNameParts = new HashSet<string>(this.XmlDocument
                .SelectNodes("//surname[string-length(normalize-space(.)) > 2]|//given-names[string-length(normalize-space(.)) > 2]")
                .Cast<XmlNode>()
                .Select(s => s.InnerText)
                .MatchWithStringList(taxaNamesFirstWord, false, true, true)
                .Select(Regex.Escape));

            var result = taxaNames.DistinctWithStringList(taxaLikePersonNameParts, true, false, true);

            return result;
        }
    }
}