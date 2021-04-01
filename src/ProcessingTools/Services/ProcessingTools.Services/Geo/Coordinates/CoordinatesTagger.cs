// <copyright file="CoordinatesTagger.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo.Coordinates
{
    using ProcessingTools.Contracts.Models.Geo.Coordinates;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Geo;
    using ProcessingTools.Contracts.Services.Geo.Coordinates;
    using ProcessingTools.Services.Abstractions;

    /// <summary>
    /// Coordinates tagger.
    /// </summary>
    public class CoordinatesTagger : StringDataMinerTagger<ICoordinatesDataMiner, ICoordinateTagModelProvider>, ICoordinatesTagger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinatesTagger"/> class.
        /// </summary>
        /// <param name="evaluator">Generic string data miner evaluator.</param>
        /// <param name="tagger">String tagger.</param>
        /// <param name="tagModelProvider">Tag model provider.</param>
        public CoordinatesTagger(IStringDataMinerEvaluator<ICoordinatesDataMiner> evaluator, IStringTagger tagger, ICoordinateTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
