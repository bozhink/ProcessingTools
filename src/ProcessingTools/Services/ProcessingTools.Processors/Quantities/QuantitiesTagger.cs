// <copyright file="QuantitiesTagger.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Quantities
{
    using ProcessingTools.Data.Miners.Contracts.Quantities;
    using ProcessingTools.Processors.Abstractions;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Contracts.Quantities;
    using ProcessingTools.Processors.Models.Contracts;

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
