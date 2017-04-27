/*
 * Deviation:
 * -36.5806 149.3153 ±100 m
 * 24 km W
 */

namespace ProcessingTools.Data.Miners.Miners.Geo
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Miners.Contracts.Miners.Geo;
    using ProcessingTools.Extensions;

    public class GeographicDeviationsDataMiner : IGeographicDeviationsDataMiner
    {
        private const string DistancePattern = @"(\d+(?:[,\.]\d+)?(?:\s*[\(\)\[\]\{\}×\*])?\s*)+?k?m";

        public async Task<IEnumerable<string>> Mine(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            const string Pattern = DistancePattern + @"\W{0,4}(?:[NSEW][NSEW\s\.-]{0,5}(?!\w)|(?i)(?:east|west|south|notrh)+)";

            var items = await content.GetMatchesAsync(new Regex(Pattern));
            var result = new HashSet<string>(items);

            return result;
        }
    }
}
