// <copyright file="ProductsTagger.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Products
{
    using ProcessingTools.Data.Miners.Contracts.Products;
    using ProcessingTools.Processors.Abstractions;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Contracts.Products;
    using ProcessingTools.Processors.Models.Contracts;

    /// <summary>
    /// Products tagger.
    /// </summary>
    public class ProductsTagger : StringDataMinerTagger<IProductsDataMiner, IProductTagModelProvider>, IProductsTagger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsTagger"/> class.
        /// </summary>
        /// <param name="evaluator">Generic string data miner evaluator.</param>
        /// <param name="tagger">String tagger.</param>
        /// <param name="tagModelProvider">Tag model provider.</param>
        public ProductsTagger(IStringDataMinerEvaluator<IProductsDataMiner> evaluator, IStringTagger tagger, IProductTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
