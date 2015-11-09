namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Concurrency;
    using ServiceClient.Bio.Aphia;

    public class AphiaTaxaRankResolver : ITaxaRankResolver
    {
        public TaxaRankResolverInternalResult Resolve(IEnumerable<string> scientificNames)
        {
            var taxonRanks = new ConcurrentQueue<ITaxonRank>();
            var taxaExceptions = new ConcurrentQueue<TaxaRankResolverException>();
            var systemExceptions = new ConcurrentQueue<Exception>();

            Parallel.ForEach(
                scientificNames,
                (scientificName, state) =>
                {
                    Delayer.Delay();

                    using (var aphiaService = new AphiaNameService())
                    {
                        var aphiaRecords = aphiaService.getAphiaRecords(scientificName, false, true, false, 0);
                        try
                        {
                            if (aphiaRecords == null || aphiaRecords.Length < 1)
                            {
                                var exception = new TaxaRankResolverException("No match or error.")
                                {
                                    Messages = new List<KeyValuePair<string, string>>()
                                };

                                exception.Messages
                                    .Add(new KeyValuePair<string, string>(scientificName, "No match or error."));

                                throw exception;
                            }

                            var ranks = new HashSet<string>(aphiaRecords
                                .Where(s => string.Compare(s.scientificname, scientificName, true) == 0)
                                .Select(s => s.rank.ToLower()));

                            if (ranks.Count > 1)
                            {
                                var exception = new TaxaRankResolverException("Multiple matches.")
                                {
                                    Messages = new List<KeyValuePair<string, string>>()
                                };

                                foreach (var record in aphiaRecords)
                                {
                                    exception.Messages
                                        .Add(new KeyValuePair<string, string>(
                                            record.scientificname,
                                            $"{record.rank}, {record.authority}"));
                                }

                                throw exception;
                            }

                            taxonRanks.Enqueue(new SimpleTaxonRank
                            {
                                ScientificName = scientificName,
                                Rank = ranks.FirstOrDefault()
                            });
                        }
                        catch (TaxaRankResolverException e)
                        {
                            taxaExceptions.Enqueue(e);
                        }
                        catch (Exception e)
                        {
                            systemExceptions.Enqueue(e);
                            state.Break();
                        }
                    }
                });

            if (systemExceptions.Count > 0)
            {
                throw new AggregateException(systemExceptions.ToList());
            }

            var result = new TaxaRankResolverInternalResult
            {
                Results = taxonRanks.ToList(),
                Exceptions = taxaExceptions.ToList()
            };

            return result;
        }

        public ITaxonRank Resolve(string scientificName)
        {
            return this.Resolve((new string[] { scientificName }).ToList()).Results.FirstOrDefault();
        }
    }
}