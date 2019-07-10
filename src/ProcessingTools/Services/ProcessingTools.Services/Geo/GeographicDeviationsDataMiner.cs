﻿// <copyright file="GeographicDeviationsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

/*
 * Deviation:
 * -36.5806 149.3153 ±100 m
 * 24 km W
 */

using ProcessingTools.Contracts.Services.Geo;

namespace ProcessingTools.Services.Geo
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Geographic deviations data miner.
    /// </summary>
    public class GeographicDeviationsDataMiner : IGeographicDeviationsDataMiner
    {
        private const string DistancePattern = @"(\d+(?:[,\.]\d+)?(?:\s*[\(\)\[\]\{\}×\*])?\s*)+?k?m";

        /// <inheritdoc/>
        public async Task<string[]> MineAsync(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            const string Pattern = DistancePattern + @"\W{0,4}(?:[NSEW][NSEW\s\.-]{0,5}(?!\w)|(?i)(?:east|west|south|north)+)";

            var data = await context.GetMatchesAsync(new Regex(Pattern)).ConfigureAwait(false);
            return data.Distinct().ToArray();
        }
    }
}
