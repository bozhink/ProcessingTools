// <copyright file="MorphologicalEpithetsTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio
{
    using ProcessingTools.Contracts.Models.Bio;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Bio;
    using ProcessingTools.Services.Abstractions;

    /// <summary>
    /// Morphological epithets tagger.
    /// </summary>
    public class MorphologicalEpithetsTagger : StringDataMinerTagger<IMorphologicalEpithetsDataMiner, IMorphologicalEpithetTagModelProvider>, IMorphologicalEpithetsTagger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MorphologicalEpithetsTagger"/> class.
        /// </summary>
        /// <param name="evaluator">Data miner evaluator.</param>
        /// <param name="tagger">String tagger.</param>
        /// <param name="tagModelProvider">Tag model provider.</param>
        public MorphologicalEpithetsTagger(IStringDataMinerEvaluator<IMorphologicalEpithetsDataMiner> evaluator, IStringTagger tagger, IMorphologicalEpithetTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
