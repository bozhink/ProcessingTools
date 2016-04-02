namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using Contracts;
    using Factories;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Contracts;

    public class LocalDbTaxaRankDataService : TaxaInformationResolverDataServiceFactory<ITaxonRank>, ILocalDbTaxaRankDataService
    {
        private XElement rankList;

        public LocalDbTaxaRankDataService(string localDbFileName)
        {
            if (string.IsNullOrWhiteSpace(localDbFileName))
            {
                throw new ArgumentNullException(nameof(localDbFileName));
            }

            this.rankList = XElement.Load(localDbFileName);
        }

        protected override void Delay()
        {
        }

        protected override Task ResolveScientificName(string scientificName, ConcurrentQueue<ITaxonRank> taxaRanks)
        {
            return Task.Run(() =>
            {
                Regex searchTaxaName = new Regex($"(?i)\\b{scientificName}\\b");
                var ranks = this.rankList.Elements()
                    .Where(t => searchTaxaName.IsMatch(t.Element("part").Element("value").Value))
                    .Select(t => t.Element("part").Element("rank").Element("value").Value);

                foreach (var rank in ranks)
                {
                    taxaRanks.Enqueue(new TaxonRankServiceModel
                    {
                        ScientificName = scientificName,
                        Rank = rank
                    });
                }
            });
        }
    }
}