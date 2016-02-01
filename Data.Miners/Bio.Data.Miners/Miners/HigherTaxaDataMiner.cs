namespace ProcessingTools.Bio.Data.Miners
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Infrastructure.Extensions;

    public class HigherTaxaDataMiner : IHigherTaxaDataMiner
    {
        private const string HigherTaxaMatchPattern = @"\b([A-Z](?i)[a-z]*(?:morphae?|mida|toda|ideae|oida|genea|formes|formea|ales|lifera|ieae|indeae|eriae|idea|aceae|oidea|oidae|inae|ini|ina|anae|ineae|acea|oideae|mycota|mycotina|mycetes|mycetidae|phyta|phytina|opsida|phyceae|idae|phycidae|ptera|poda|phaga|itae|odea|alia|ntia|osauria))\b";

        private readonly Regex matchHigherTaxa = new Regex(HigherTaxaMatchPattern);

        private ITaxonomicListDataService<string> whiteList;

        public HigherTaxaDataMiner()
            : this(null)
        {
        }

        public HigherTaxaDataMiner(ITaxonomicListDataService<string> whiteList)
        {
            this.whiteList = whiteList;
        }

        public async Task<IQueryable<string>> Mine(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException("content");
            }

            string textToMine = string.Join(" ", content.ExtractWordsFromString());

            // Match plausible higher taxa by pattern.
            var items = new List<string>(await textToMine.GetMatchesAsync(this.matchHigherTaxa));

            if (this.whiteList != null)
            {
                items.AddRange(this.MatchWithWhiteList(textToMine));
            }

            var result = new HashSet<string>(items);
            return result.AsQueryable();
        }

        private IEnumerable<string> MatchWithWhiteList(string textToMine)
        {
            var whiteListMatches = textToMine.MatchWithStringList(this.whiteList.All().ToList(), false, false, false);
            return whiteListMatches;
        }
    }
}