// <copyright file="GeographicDeviationsTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services;
using ProcessingTools.Contracts.Services.Geo;
using ProcessingTools.Contracts.Services.Models.Geo;

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Services.Abstractions;

    /// <summary>
    /// Geographic deviations tagger.
    /// </summary>
    public class GeographicDeviationsTagger : StringDataMinerTagger<IGeographicDeviationsDataMiner, IGeographicDeviationTagModelProvider>, IGeographicDeviationsTagger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeographicDeviationsTagger"/> class.
        /// </summary>
        /// <param name="evaluator">Generic string data miner evaluator.</param>
        /// <param name="tagger">String tagger.</param>
        /// <param name="tagModelProvider">Tag model provider.</param>
        public GeographicDeviationsTagger(IStringDataMinerEvaluator<IGeographicDeviationsDataMiner> evaluator, IStringTagger tagger, IGeographicDeviationTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
