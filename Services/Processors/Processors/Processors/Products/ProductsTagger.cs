namespace ProcessingTools.Processors.Products
{
    using Contracts;
    using Contracts.Products;
    using Contracts.Providers;
    using Generics;
    using ProcessingTools.Data.Miners.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class ProductsTagger : GenericStringMinerTagger<IProductsDataMiner, IProductTagModelProvider>, IProductsTagger
    {
        public ProductsTagger(IGenericStringDataMinerEvaluator<IProductsDataMiner> evaluator, IStringTagger tagger, IProductTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
