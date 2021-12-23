// <copyright file="SpecimenCountTagger.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio
{
    using ProcessingTools.Contracts.Models.Bio;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Bio;
    using ProcessingTools.Services.Abstractions;

    /// <summary>
    /// Specimen count tagger.
    /// </summary>
    public class SpecimenCountTagger : StringDataMinerTagger<ISpecimenCountDataMiner, ISpecimenCountTagModelProvider>, ISpecimenCountTagger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecimenCountTagger"/> class.
        /// </summary>
        /// <param name="evaluator">Data miner evaluator.</param>
        /// <param name="tagger">String tagger.</param>
        /// <param name="tagModelProvider">Tag model provider.</param>
        public SpecimenCountTagger(IStringDataMinerEvaluator<ISpecimenCountDataMiner> evaluator, IStringTagger tagger, ISpecimenCountTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
