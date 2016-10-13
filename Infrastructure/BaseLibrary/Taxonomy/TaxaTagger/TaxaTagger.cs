namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Bio.Taxonomy.Constants;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Contracts;
    using ProcessingTools.Strings.Extensions;

    public abstract class TaxaTagger : IDocumentTagger
    {
        private const string SelectPersonNamesXPath = "//surname[string-length(normalize-space(.)) > 2]|//given-names[string-length(normalize-space(.)) > 2]";

        private readonly IBiotaxonomicBlackListIterableDataService service;

        public TaxaTagger(IBiotaxonomicBlackListIterableDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        public abstract Task<object> Tag(IDocument document);

        protected async Task<IEnumerable<string>> ClearFakeTaxaNames(IDocument document, IEnumerable<string> taxaNames)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (taxaNames == null)
            {
                throw new ArgumentNullException(nameof(taxaNames));
            }

            var taxaNamesWithoutPersonNames = await this.ClearFakeTaxaNamesLikePersonNamesInArticle(document, taxaNames);
            var taxaNamesWithoutBlackListed = await this.ClearFakeTaxaNamesUsingBlackList(taxaNamesWithoutPersonNames);

            var result = new HashSet<string>(taxaNamesWithoutBlackListed);

            return result;
        }

        protected XmlElement CreateNewTaxonNameXmlElement(IDocument document, TaxonType type)
        {
            XmlElement tn = document.XmlDocument.CreateElement(XmlInternalSchemaConstants.TaxonNameElementName);
            tn.SetAttribute(XmlInternalSchemaConstants.TaxonNameTypeAttributeName, type.ToString().ToLower());
            return tn;
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

        private async Task<IEnumerable<string>> ClearFakeTaxaNamesLikePersonNamesInArticle(IDocument document, IEnumerable<string> taxaNames)
        {
            var taxaNamesFirstWord = await this.GetTaxaNamesFirstWords(taxaNames);

            var personNames = document.SelectNodes(SelectPersonNamesXPath)
                .Select(s => s.InnerText)
                .Select(Regex.Escape);

            var taxaLikePersonNameParts = personNames.MatchWithStringList(taxaNamesFirstWord, false, true, true);

            var result = taxaNames.DistinctWithStringList(taxaLikePersonNameParts, true, false, true);

            return result;
        }
    }
}