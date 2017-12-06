namespace ProcessingTools.Processors.Processors.Products
{
    using ProcessingTools.Data.Miners.Contracts.Miners.Products;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Contracts.Processors;
    using ProcessingTools.Contracts.Processors.Processors.Products;
    using ProcessingTools.Contracts.Processors.Providers;
    using ProcessingTools.Processors.Generics;

    public class ProductsTagger : GenericStringMinerTagger<IProductsDataMiner, IProductTagModelProvider>, IProductsTagger
    {
        public ProductsTagger(IGenericStringDataMinerEvaluator<IProductsDataMiner> evaluator, IStringTagger tagger, IProductTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
