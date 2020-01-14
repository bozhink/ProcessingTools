// <copyright file="QuantitiesTagger.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Quantities
{
    using ProcessingTools.Contracts.Models.Quantities;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Quantities;
    using ProcessingTools.Services.Abstractions;

    /// <summary>
    /// Quantities tagger.
    /// </summary>
    public class QuantitiesTagger : StringDataMinerTagger<IQuantitiesDataMiner, IQuantityTagModelProvider>, IQuantitiesTagger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuantitiesTagger"/> class.
        /// </summary>
        /// <param name="evaluator">Generic string data miner evaluator.</param>
        /// <param name="tagger">String tagger.</param>
        /// <param name="tagModelProvider">Tag model provider.</param>
        public QuantitiesTagger(IStringDataMinerEvaluator<IQuantitiesDataMiner> evaluator, IStringTagger tagger, IQuantityTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
