namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;

    using Contracts;
    using Models;

    public class LocalDbTaxaRankDataService : TaxaRankDataService
    {
        private XElement rankList;

        public LocalDbTaxaRankDataService(string localDbFileName)
        {
            this.rankList = XElement.Load(localDbFileName);
        }

        protected override void Delay()
        {
        }

        protected override void ResolveRank(string scientificName, ConcurrentQueue<ITaxonRank> taxaRanks)
        {
            Regex searchTaxaName = new Regex($"(?i)\\b{scientificName}\\b");
            var ranks = this.rankList.Elements()
                .Where(t => searchTaxaName.IsMatch(t.Element("part").Element("value").Value))
                .Select(t => t.Element("part").Element("rank").Element("value").Value);

            foreach (var rank in ranks)
            {
                taxaRanks.Enqueue(new TaxonRankDataServiceResponseModel
                {
                    ScientificName = scientificName,
                    Rank = rank
                });
            }
        }
    }
}