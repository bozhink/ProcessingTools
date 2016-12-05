namespace ProcessingTools.Processors.Processors.Products
{
    using ProcessingTools.Data.Miners.Contracts.Miners.Products;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Contracts.Products;
    using ProcessingTools.Processors.Contracts.Providers;
    using ProcessingTools.Processors.Generics;

    public class ProductsTagger : GenericStringMinerTagger<IProductsDataMiner, IProductTagModelProvider>, IProductsTagger
    {
        public ProductsTagger(IGenericStringDataMinerEvaluator<IProductsDataMiner> evaluator, IStringTagger tagger, IProductTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
