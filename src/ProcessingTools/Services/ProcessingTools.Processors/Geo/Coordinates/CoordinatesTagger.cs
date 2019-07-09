﻿// <copyright file="CoordinatesTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Geo.Coordinates
{
    using ProcessingTools.Services.Contracts.Geo;
    using ProcessingTools.Processors.Abstractions;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Contracts.Geo.Coordinates;
    using ProcessingTools.Processors.Models.Contracts;

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
