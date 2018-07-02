﻿// <copyright file="CoordinatesDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Geo
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Miners.Contracts.Geo;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Coordinates data miner.
    /// </summary>
    public class CoordinatesDataMiner : ICoordinatesDataMiner
    {
        /// <inheritdoc/>
        public async Task<string[]> MineAsync(string context)
        {
            var internalMiner = new InternalMiner(Regex.Replace(context, @"(?:[º°˚]|(?<=\d\s?)o(?![A-Za-z]))", "°"));

            var result = new ConcurrentQueue<string>();
            var methods = typeof(InternalMiner).GetMethods()
                .Where(m => !m.IsConstructor && m.IsPublic && m.ReturnType == typeof(Task<IEnumerable<string>>))
                .ToList();
            foreach (var method in methods)
            {
                var task = (Task<IEnumerable<string>>)method.Invoke(internalMiner, null);
                var items = await task.ConfigureAwait(false);
                items.AsParallel()
                    .ForAll(item => result.Enqueue(item.Trim(' ', ';', ',', ':')));
            }

            return new HashSet<string>(result).ToArray();
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

                return await this.content.GetMatchesAsync(new Regex(Pattern)).ConfigureAwait(false);
            }

            public async Task<IEnumerable<string>> GetDecimalNumberDirectionCoordinates()
            {
                const string Pattern = @"((?:(?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?)\W{0,3}[NSEWO])\s*\W{0,3}?\s*(?:(?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?)\W{0,3}[NSEWO]))";

                return await this.content.GetMatchesAsync(new Regex(Pattern)).ConfigureAwait(false);
            }

            public async Task<IEnumerable<string>> GetDecimalNumberDegDirectionCoordinates()
            {
                const string Pattern = @"((?:(?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?\s*°\s*).{0,20}?[NSEWO])\s*\W{0,3}?\s*(?:(?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?\s*°\s*).{0,20}?[NSEWO]))";

                return await this.content.GetMatchesAsync(new Regex(Pattern)).ConfigureAwait(false);
            }

            public async Task<IEnumerable<string>> GetDecimalNumberCoordinates()
            {
                const string Pattern = @"((?:[–—−-]?\s{0,2}\b[0-1]?[0-9]{1,2}[,\.][0-9]{1,6}\b)\s*[;,\s]\s*(?:[–—−-]?\s{0,2}\b[0-1]?[0-9]{1,2}[,\.][0-9]{1,6}\b))";

                return await this.content.GetMatchesAsync(new Regex(Pattern)).ConfigureAwait(false);
            }
        }
    }
}
