/*
 * 6–8 m depth
 * Elevation: 2880 m
 * Elevation: 2900
 */

namespace ProcessingTools.Harvesters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Contracts;
    using Extensions;

    public class AltitudesHarvester : IAltitudesHarvester
    {
        private const string DistancePattern = @"(\d+(?:[,\.]\d+)?(?:\s*[\(\)\[\]\{\}×\*])?\s*)+?k?m";

        public async Task<IQueryable<string>> Harvest(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException("content");
            }

            var internalHarvester = new InternalHarvester(content);
            var distanceAltitude = await internalHarvester.HarvestDistenceAltitude();
            var altitudeDistance = await internalHarvester.HarvestAltitudeDistence();

            var items = new List<string>();
            items.AddRange(distanceAltitude);
            items.AddRange(altitudeDistance);

            var result = new HashSet<string>(items);

            return result.AsQueryable();
        }

        private class InternalHarvester
        {
            private string content;

            public InternalHarvester(string content)
            {
                this.content = content;
            }

            /// <summary>
            /// Extracts altitudes of type distance + altitude suffix.
            /// </summary>
            /// <returns>IEnumerable of matches items.</returns>
            /// <example>510–650 m a.s.l.</example>
            /// <example>58 m alt.</example>
            public async Task<IEnumerable<string>> HarvestDistenceAltitude()
            {
                const string Pattern = DistancePattern + @"\W{0,4}(?i)(?:a\W*s\W*l|a\W*l\W*t)[^\w<>]?";

                return await this.content.GetMatchesAsync(new Regex(Pattern));
            }

            /// <summary>
            /// Extracts altitudes of type altitude prefix + distance.
            /// </summary>
            /// <returns>IEnumerable of matches items.</returns>
            /// <example>alt. ca. 271 m</example>
            public async Task<IEnumerable<string>> HarvestAltitudeDistence()
            {
                const string Pattern = @"(?:(?i)a\W*l\W*t(?:[^\w<>]{0,3}c\W*a)?)[^\w<>]{0,5}" + DistancePattern;

                return await this.content.GetMatchesAsync(new Regex(Pattern));
            }
        }
    }
}