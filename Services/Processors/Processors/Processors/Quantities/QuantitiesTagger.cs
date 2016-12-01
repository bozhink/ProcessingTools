namespace ProcessingTools.Processors.Quantities
{
    using Contracts;
    using Contracts.Providers;
    using Contracts.Quantities;
    using Generics;
    using ProcessingTools.Data.Miners.Contracts.Miners;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class QuantitiesTagger : GenericStringMinerTagger<IQuantitiesDataMiner, IQuantityTagModelProvider>, IQuantitiesTagger
    {
        public QuantitiesTagger(IGenericStringDataMinerEvaluator<IQuantitiesDataMiner> evaluator, IStringTagger tagger, IQuantityTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
