namespace ProcessingTools.Harvesters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Contracts;
    using Extensions;

    public class PmcIdsHarvester : IPmcIdsHarvester
    {
        private ICollection<string> data;

        public PmcIdsHarvester()
        {
            this.data = new HashSet<string>();
        }

        public IQueryable<string> Data
        {
            get
            {
                return this.data.AsQueryable();
            }
        }

        public void Harvest(string content)
        {
            var items = new List<string>();

            const string PmcIdsPattern = @"(?i)\b(?:pmid|pmc|pmcid)\W{0,5}\d+";
            Regex matchPmcIds = new Regex(PmcIdsPattern);
            items.AddRange(content.GetMatches(matchPmcIds));

            this.data = new HashSet<string>(items);
        }
    }
}