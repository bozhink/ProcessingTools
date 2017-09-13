/*
 * 6–8 m depth
 * Elevation: 2880 m
 * Elevation: 2900
 */

namespace ProcessingTools.Data.Miners.Miners.Geo
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Common.Extensions.Linq;
    using ProcessingTools.Data.Miners.Contracts.Miners.Geo;

    public class AltitudesDataMiner : IAltitudesDataMiner
    {
        private const string DistancePattern = @"(\d+(?:[,\.]\d+)?(?:\s*[\(\)\[\]\{\}×\*])?\s*)+?k?m";

        public async Task<IEnumerable<string>> MineAsync(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            var patterns = new string[]
            {
                DistancePattern + @"\W{0,4}(?i)(?:a\W*s\W*l|a\W*l\W*t)[^\w<>]?",
                @"(?:(?i)a\W*l\W*t(?:[^\w<>]{0,3}c\W*a)?)[^\w<>]{0,5}" + DistancePattern
            };

            var data = await this.ExtractData(context, patterns).ToListAsync().ConfigureAwait(false);

            var result = new HashSet<string>(data);
            return result;
        }

        private IEnumerable<string> ExtractData(string content, IEnumerable<string> patterns)
        {
            var data = new ConcurrentBag<string>();

            patterns
                .AsParallel()
                .ForAll((pattern) =>
                {
                    content.GetMatches(new Regex(pattern))
                        .Select(m => m.Trim())
                        .ToList()
                        .ForEach(s =>
                        {
                            data.Add(s);
                        });
                });

            return data;
        }
    }
}
