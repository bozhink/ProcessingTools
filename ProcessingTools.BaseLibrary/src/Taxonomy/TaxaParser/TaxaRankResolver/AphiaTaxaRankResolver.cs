namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using ServiceClient.Bio.Aphia;

    public class AphiaTaxaRankResolver : ITaxaRankResolver
    {
        public IEnumerable<ITaxonRank> Resolve(IEnumerable<string> scientificNames)
        {
            var aphiaService = new AphiaNameService();
            var result = new HashSet<SimpleTaxonRank>();

            foreach (string scientificName in scientificNames)
            {
                var aphiaRecords = aphiaService.getAphiaRecords(scientificName, false, true, false, 0);

                if (aphiaRecords == null || aphiaRecords.Length < 1)
                {
                    // this.logger?.Log($"{scientificName} --> No match or error.");
                }
                else
                {
                    var ranks = new HashSet<string>(aphiaRecords
                        .Where(x => string.Compare(x.scientificname, scientificName, true) == 0)
                        .Select(x => x.rank.ToLower()));

                    if (ranks.Count > 1)
                    {
                        ////this.logger?.Log($"WARNING:\n{scientificName} --> Multiple matches:");
                        ////foreach (var record in aphiaRecords)
                        ////{
                        ////    this.logger?.Log($"{record.scientificname} --> {record.rank}, {record.authority}");
                        ////}
                    }
                    else
                    {
                        result.Add(new SimpleTaxonRank
                        {
                            ScientificName = scientificName,
                            Rank = ranks.FirstOrDefault()
                        });
                    }
                }
            }

            return result;
        }

        public ITaxonRank Resolve(string scientificName)
        {
            return this.Resolve((new string[] { scientificName }).ToList()).FirstOrDefault();
        }
    }
}