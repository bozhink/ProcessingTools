namespace ProcessingTools.Geo.Data.Miners
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Contracts;
    using ProcessingTools.Extensions;

    public class CoordinatesDataMiner : ICoordinatesDataMiner
    {
        public async Task<IQueryable<string>> Mine(string content)
        {
            var internalMiner = new InternalMiner(Regex.Replace(content, @"(?:[º°˚]|(?<=\d\s?)o(?![A-Za-z]))", "°"));

            var result = new ConcurrentQueue<string>();
            var methods = typeof(InternalMiner).GetMethods()
                .Where(m => !m.IsConstructor && m.IsPublic && m.ReturnType == typeof(Task<IEnumerable<string>>))
                .ToList();
            foreach (var method in methods)
            {
                var items = (Task<IEnumerable<string>>)method.Invoke(internalMiner, null);
                (await items).AsParallel().ForAll(item => result.Enqueue(item));
            }

            return new HashSet<string>(result).AsQueryable();
        }

        private class InternalMiner
        {
            private readonly string content;

            public InternalMiner(string content)
            {
                if (string.IsNullOrWhiteSpace(content))
                {
                    throw new ArgumentNullException(nameof(content));
                }

                this.content = content;
            }

            public async Task<IEnumerable<string>> GetDirectionNumberCoordinates()
            {
                const string Pattern = @"((?:[NSEWO](?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?\s*°\s*)(?:(?<!\d)[0-6]?[0-9](?:\s*[,\.]\s*\d+)?\W*?){0,2})\s*?\W{0,3}?\s*(?:[NSEWO](?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?\s*°\s*)(?:(?<!\d)[0-6]?[0-9](?:\s*[,\.]\s*\d+)?\W*?){0,2}))";

                return await this.content.GetMatchesAsync(new Regex(Pattern));
            }

            public async Task<IEnumerable<string>> GetDecimalNumberDirectionCoordinates()
            {
                const string Pattern = @"((?:(?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?)\W{0,3}[NSEWO])\s*\W{0,3}?\s*(?:(?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?)\W{0,3}[NSEWO]))";

                return await this.content.GetMatchesAsync(new Regex(Pattern));
            }

            public async Task<IEnumerable<string>> GetDecimalNumberDegDirectionCoordinates()
            {
                const string Pattern = @"((?:(?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?\s*°\s*).{0,20}?[NSEWO])\s*\W{0,3}?\s*(?:(?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?\s*°\s*).{0,20}?[NSEWO]))";

                return await this.content.GetMatchesAsync(new Regex(Pattern));
            }

            public async Task<IEnumerable<string>> GetDecimalNumberCoordinates()
            {
                const string Pattern = @"((?:[–—−-]?\s{0,2}\b[0-1]?[0-9]{1,2}[,\.][0-9]{3,6}\b)\s*[;,]\s*(?:[–—−-]?\s{0,2}\b[0-1]?[0-9]{1,2}[,\.][0-9]{3,6}\b))";

                return await this.content.GetMatchesAsync(new Regex(Pattern));
            }

            public async Task<IEnumerable<string>> GetDecimalNumberLatitudeLongitudeCoordinates()
            {
                const string Pattern = @"(?<latitude>(?:\b[–—−-]\W?|\b)[0-9]?[0-9]\b[\s\.]{1,3}[0-9]+)[^\w\.]{1,5}(?<longitude>(?:\b[–—−-]\W?|\b)(?:[0-1][0-8]|[0-9])?[0-9]\b[\s\.]{1,3}[0-9]+)";

                return await this.content.GetMatchesAsync(new Regex(Pattern));
            }
        }
    }
}