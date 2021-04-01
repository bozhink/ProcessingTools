﻿// <copyright file="HigherTaxaDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Extensions.Text;

    /// <summary>
    /// Higher taxa data miner.
    /// </summary>
    public class HigherTaxaDataMiner : IHigherTaxaDataMiner
    {
        private readonly Regex matchHigherTaxa = new Regex(TaxaRegexPatterns.HigherTaxaMatchPattern);

        /// <inheritdoc/>
        public Task<string[]> MineAsync(string context, IEnumerable<string> seed, IEnumerable<string> stopWords)
        {
            return Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(context))
                {
                    return Array.Empty<string>();
                }

                var words = context.ExtractWordsFromText()
                    .DistinctWithStopWords(stopWords)
                    .ToArray();

                var result = words.Where(w => this.matchHigherTaxa.IsMatch(w))
                    .Union(words.MatchWithSeedWords(seed))
                    .ToArray();

                return new HashSet<string>(result).ToArray();
            });
        }
    }
}
