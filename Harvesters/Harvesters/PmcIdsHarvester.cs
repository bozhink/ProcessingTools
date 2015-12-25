namespace ProcessingTools.Harvesters
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using Contracts;
    using Extensions;

    using ProcessingTools.Harvesters.Common.Factories;

    public class PmcIdsHarvester : StringHarvesterFactory, IPmcIdsHarvester
    {
        public override void Harvest(string content)
        {
            var items = new List<string>();

            const string PmcIdsPattern = @"(?i)\b(?:pmid|pmc|pmcid)\W{0,5}\d+";
            Regex matchPmcIds = new Regex(PmcIdsPattern);
            items.AddRange(content.GetMatches(matchPmcIds));

            this.Items = new HashSet<string>(items);
        }
    }
}