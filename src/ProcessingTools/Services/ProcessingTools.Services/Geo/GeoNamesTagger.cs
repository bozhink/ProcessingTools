// <copyright file="GeoNamesTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Geo;
    using ProcessingTools.Contracts.Services.Models.Geo;
    using ProcessingTools.Services.Abstractions;

    /// <summary>
    /// Geo names tagger.
    /// </summary>
    public class GeoNamesTagger : StringDataMinerTagger<IGeoNamesDataMiner, IGeoNameTagModelProvider>, IGeoNamesTagger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeoNamesTagger"/> class.
        /// </summary>
        /// <param name="evaluator">Generic string data miner evaluator.</param>
        /// <param name="tagger">String tagger.</param>
        /// <param name="tagModelProvider">Tag model provider.</param>
        public GeoNamesTagger(IStringDataMinerEvaluator<IGeoNamesDataMiner> evaluator, IStringTagger tagger, IGeoNameTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
