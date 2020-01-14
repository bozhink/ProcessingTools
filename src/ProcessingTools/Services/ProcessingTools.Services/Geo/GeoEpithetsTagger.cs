// <copyright file="GeoEpithetsTagger.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Geo;
    using ProcessingTools.Services.Abstractions;

    /// <summary>
    /// Geo epithets tagger.
    /// </summary>
    public class GeoEpithetsTagger : StringDataMinerTagger<IGeoEpithetsDataMiner, IGeoEpithetTagModelProvider>, IGeoEpithetsTagger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeoEpithetsTagger"/> class.
        /// </summary>
        /// <param name="evaluator">Generic string data miner evaluator.</param>
        /// <param name="tagger">String tagger.</param>
        /// <param name="tagModelProvider">Tag model provider.</param>
        public GeoEpithetsTagger(IStringDataMinerEvaluator<IGeoEpithetsDataMiner> evaluator, IStringTagger tagger, IGeoEpithetTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
