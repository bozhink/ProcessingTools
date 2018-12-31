// <copyright file="SpecimenCountTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Bio
{
    using ProcessingTools.Data.Miners.Contracts.Bio;
    using ProcessingTools.Processors.Abstractions;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Contracts.Bio;
    using ProcessingTools.Processors.Models.Contracts;

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
