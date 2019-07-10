// <copyright file="DatesTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services;
using ProcessingTools.Contracts.Services.Dates;
using ProcessingTools.Contracts.Services.Models.Dates;

namespace ProcessingTools.Services.Dates
{
    using ProcessingTools.Services.Abstractions;

    /// <summary>
    /// Dates tagger.
    /// </summary>
    public class DatesTagger : StringDataMinerTagger<IDatesDataMiner, IDateTagModelProvider>, IDatesTagger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatesTagger"/> class.
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
