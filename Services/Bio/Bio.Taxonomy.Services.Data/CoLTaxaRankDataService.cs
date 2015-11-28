namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Xml;

    using Contracts;
    using Models;
    using ServiceClient.Bio.CatalogueOfLife;

    public class CoLTaxaRankDataService : TaxaRankDataService
    {
        protected override void ResolveRank(string scientificName, ConcurrentQueue<ITaxonRank> taxaRanks)
        {
            XmlDocument colResponse = CatalogueOfLifeDataRequester.SearchCatalogueOfLife(scientificName).Result;

            string xpath = $"/results/result[normalize-space(translate(name,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'))='{scientificName.ToLower()}']";

            XmlNodeList responseItems = colResponse.SelectNodes(xpath);

            if (responseItems != null && responseItems.Count > 0)
            {
                var ranks = responseItems
                    .Cast<XmlNode>()
                    .Select(c => c["rank"].InnerText.ToLower())
                    .Distinct()
                    .ToList();

                foreach (var rank in ranks)
                {
                    taxaRanks.Enqueue(new TaxonRank
                    {
                        ScientificName = scientificName,
                        Rank = rank
                    });
                }
            }
        }
    }
}