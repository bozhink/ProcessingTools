// <copyright file="DatesTagger.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Dates
{
    using ProcessingTools.Data.Miners.Contracts.Miners.Dates;
    using ProcessingTools.Processors.Abstractions;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Contracts.Dates;
    using ProcessingTools.Processors.Contracts.Layout;
    using ProcessingTools.Processors.Models.Contracts;

    /// <summary>
    /// Dates tagger.
    /// </summary>
    public class DatesTagger : StringDataMinerTagger<IDatesDataMiner, IDateTagModelProvider>, IDatesTagger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatesTagger"/> class.s
        /// </summary>
        /// <param name="evaluator">Generic string data miner evaluator.</param>
        /// <param name="tagger">String tagger.</param>
        /// <param name="tagModelProvider">Tag model provider.</param>
        public DatesTagger(IStringDataMinerEvaluator<IDatesDataMiner> evaluator, IStringTagger tagger, IDateTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
