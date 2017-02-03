namespace ProcessingTools.Processors.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts.Providers;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Harvesters.Contracts.Harvesters.Meta;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

    public class TaxaStopWordsProvider : ITaxaStopWordsProvider
    {
        private readonly IPersonNamesHarvester personNamesHarvester;
        private readonly IBlackList blacklist;

        public TaxaStopWordsProvider(
            IPersonNamesHarvester personNamesHarvester,
            IBlackList blacklist)
        {
            if (personNamesHarvester == null)
            {
                throw new ArgumentNullException(nameof(personNamesHarvester));
            }

            if (blacklist == null)
            {
                throw new ArgumentNullException(nameof(blacklist));
            }

            this.personNamesHarvester = personNamesHarvester;
            this.blacklist = blacklist;
        }

        public async Task<IEnumerable<string>> GetStopWords(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var personNames = await this.personNamesHarvester.Harvest(context);
            var blacklistItems = await this.blacklist.Items;

            var stopWords = await personNames
                .SelectMany(n => new string[] { n.GivenNames, n.Surname, n.Suffix, n.Prefix })
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Union(blacklistItems)
                .Select(w => w.ToLower())
                .Distinct()
                .ToArrayAsync();

            return stopWords;
        }
    }
}
