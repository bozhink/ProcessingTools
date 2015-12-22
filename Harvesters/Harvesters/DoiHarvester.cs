namespace ProcessingTools.Harvesters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Contracts;
    using Extensions;

    public class DoiHarvester : IDoiHarvester
    {
        private ICollection<string> data;

        public DoiHarvester()
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

            const string ValidDoiPattern = @"10\.[0-9]{4,5}/[^\s]+(?![,;\.]\s)";
            Regex matchValidDoi = new Regex(ValidDoiPattern);
            items.AddRange(content.GetMatches(matchValidDoi));

            const string PlausibleDoiPattern = @"(?i)\bdoi\b\W{1,5}\w[^\s]+(?![,;\.]\s)";
            Regex matchPlausibleDoi = new Regex(PlausibleDoiPattern);
            items.AddRange(content.GetMatches(matchPlausibleDoi));

            const string StripDoiPrefixPattern = @"\A(?i)\bdoi\b\W{1,5}";
            Regex stripDoiPrefix = new Regex(StripDoiPrefixPattern);
            this.data = new HashSet<string>(items
                .Select(d => stripDoiPrefix.Replace(d, string.Empty)));
        }
    }
}