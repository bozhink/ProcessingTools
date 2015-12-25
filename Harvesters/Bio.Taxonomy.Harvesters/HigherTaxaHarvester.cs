﻿namespace ProcessingTools.Bio.Taxonomy.Harvesters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Contracts;
    using Extensions;

    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Harvesters.Common.Factories;

    public class HigherTaxaHarvester : StringHarvesterFactory, IHigherTaxaHarvester
    {
        private const string HigherTaxaMatchPattern = @"\b([A-Z](?i)[a-z]*(?:morphae?|mida|toda|ideae|oida|genea|formes|formea|ales|lifera|ieae|indeae|eriae|idea|aceae|oidea|oidae|inae|ini|ina|anae|ineae|acea|oideae|mycota|mycotina|mycetes|mycetidae|phyta|phytina|opsida|phyceae|idae|phycidae|ptera|poda|phaga|itae|odea|alia|ntia|osauria))\b";

        private readonly Regex matchHigherTaxa = new Regex(HigherTaxaMatchPattern);

        private IRepositoryDataService<string> whiteList;

        public HigherTaxaHarvester()
            : this(null)
        {
        }

        public HigherTaxaHarvester(IRepositoryDataService<string> whiteList)
            : base()
        {
            this.whiteList = whiteList;
        }

        public override void Harvest(string content)
        {
            string textToMine = string.Join(" ", content.ExtractWordsFromString());

            // Match plausible higher taxa by pattern.
            this.Items = new HashSet<string>(textToMine.GetMatches(this.matchHigherTaxa));

            if (this.whiteList != null)
            {
                this.MatchWithWhiteList(textToMine);
            }
        }

        private void MatchWithWhiteList(string textToMine)
        {
            var whiteListMatches = textToMine.MatchWithStringList(this.whiteList.All().ToList(), false, false, false);
            foreach (var item in whiteListMatches)
            {
                this.Items.Add(item);
            }
        }
    }
}