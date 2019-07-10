// <copyright file="ProductsTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Products
{
    using ProcessingTools.Services.Contracts.Products;
    using ProcessingTools.Services.Abstractions;
    using ProcessingTools.Services.Contracts;
    using ProcessingTools.Services.Contracts.Products;
    using ProcessingTools.Services.Models.Contracts;

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
