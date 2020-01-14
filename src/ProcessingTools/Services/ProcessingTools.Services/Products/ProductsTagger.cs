// <copyright file="ProductsTagger.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Products
{
    using ProcessingTools.Contracts.Models.Products;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Products;
    using ProcessingTools.Services.Abstractions;

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
