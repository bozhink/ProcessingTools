namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Infrastructure.Concurrency;
    using ServiceClient.Bio.Aphia;

    public class AphiaTaxaRankResolver : ITaxaRankResolver
    {
        public TaxaRankResolverComplexResult Resolve(IEnumerable<string> scientificNames)
        {
            var result = new HashSet<SimpleTaxonRank>();

            var exceptions = new ConcurrentQueue<TaxaRankResolverException>();

            using (var aphiaService = new AphiaNameService())
            {
                // TODO: make this foreach parallel
                foreach (string scientificName in scientificNames)
                {
                    Delayer.Delay();

                    var aphiaRecords = aphiaService.getAphiaRecords(scientificName, false, true, false, 0);

                    if (aphiaRecords == null || aphiaRecords.Length < 1)
                    {
                        exceptions.Enqueue(new TaxaRankResolverException() {
                            Severity = ExceptionSeverity.Exception,
                            Messages = new List<KeyValuePair<string, string>>
                            {
                                new KeyValuePair<string, string>(scientificName, "No match or error.")
                            }
                        });
                    }
                    else
                    {
                        var ranks = new HashSet<string>(aphiaRecords
                            .Where(x => string.Compare(x.scientificname, scientificName, true) == 0)
                            .Select(x => x.rank.ToLower()));

                        if (ranks.Count > 1)
                        {
                            var exception = new TaxaRankResolverException("Multiple matches.")
                            {
                                Severity = ExceptionSeverity.Warning,
                                Messages = new List<KeyValuePair<string, string>>()
                            };

                            foreach (var record in aphiaRecords)
                            {
                                exception.Messages.Add(new KeyValuePair<string, string>(
                                    record.scientificname,
                                    $"{record.rank}, {record.authority}"));
                            }

                            exceptions.Enqueue(exception);
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
            }

            var complexResult = new TaxaRankResolverComplexResult
            {
                Result = result,
                Error = exceptions.ToList()
            };

            return complexResult;
        }

        public ITaxonRank Resolve(string scientificName)
        {
            return this.Resolve((new string[] { scientificName }).ToList()).Result.FirstOrDefault();
        }
    }
}