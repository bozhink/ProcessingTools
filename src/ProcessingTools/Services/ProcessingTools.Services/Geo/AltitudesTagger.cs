// <copyright file="AltitudesTagger.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Geo;
    using ProcessingTools.Services.Abstractions;

    /// <summary>
    /// Altitudes tagger.
    /// </summary>
    public class AltitudesTagger : StringDataMinerTagger<IAltitudesDataMiner, IAltitudeTagModelProvider>, IAltitudesTagger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AltitudesTagger"/> class.
        /// </summary>
        /// <param name="evaluator">Generic string data miner evaluator.</param>
        /// <param name="tagger">String tagger.</param>
        /// <param name="tagModelProvider">Tag model provider.</param>
        public AltitudesTagger(IStringDataMinerEvaluator<IAltitudesDataMiner> evaluator, IStringTagger tagger, IAltitudeTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
