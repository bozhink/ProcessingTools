namespace ProcessingTools.Processors.Processors.Products
{
    using Contracts;
    using Contracts.Processors.Products;
    using Contracts.Providers;
    using Generics;
    using ProcessingTools.Data.Miners.Contracts.Miners.Products;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class ProductsTagger : GenericStringMinerTagger<IProductsDataMiner, IProductTagModelProvider>, IProductsTagger
    {
        public ProductsTagger(IGenericStringDataMinerEvaluator<IProductsDataMiner> evaluator, IStringTagger tagger, IProductTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
