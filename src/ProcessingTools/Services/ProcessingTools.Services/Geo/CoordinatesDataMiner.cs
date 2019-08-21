// <copyright file="CoordinatesDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services.Geo;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Coordinates data miner.
    /// </summary>
    public class CoordinatesDataMiner : ICoordinatesDataMiner
    {
        /// <inheritdoc/>
        public Task<IList<string>> MineAsync(string context)
        {
            string[] patterns = new[]
            {
                // direction number coordinates
                @"((?:[NSEWO](?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?\s*°\s*)(?:(?<!\d)[0-6]?[0-9](?:\s*[,\.]\s*\d+)?\W*?){0,2})\s*?\W{0,3}?\s*(?:[NSEWO](?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?\s*°\s*)(?:(?<!\d)[0-6]?[0-9](?:\s*[,\.]\s*\d+)?\W*?){0,2}))",

                // decimal number direction coordinates
                @"((?:(?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?)\W{0,3}[NSEWO])\s*\W{0,3}?\s*(?:(?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?)\W{0,3}[NSEWO]))",

                // decimal number deg direction coordinates
                @"((?:(?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?\s*°\s*).{0,20}?[NSEWO])\s*\W{0,3}?\s*(?:(?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?\s*°\s*).{0,20}?[NSEWO]))",

                // decimal number coordinates
                @"((?:[–—−-]?\s{0,2}\b[0-1]?[0-9]{1,2}[,\.][0-9]{1,6}\b)\s*[;,\s]\s*(?:[–—−-]?\s{0,2}\b[0-1]?[0-9]{1,2}[,\.][0-9]{1,6}\b))",
            };

            return Task.Run<IList<string>>(() =>
            {
                string content = Regex.Replace(context, @"(?:[º°˚]|(?<=\d\s?)o(?![A-Za-z]))", "°");

                var result = new HashSet<string>();
                foreach (var pattern in patterns)
                {
                    var items = content.GetMatches(new Regex(pattern));
                    items.AsParallel().ForAll(item => result.Add(item.Trim(' ', ';', ',', ':')));
                }

                return result.ToArray();
            });
        }
    }
}
