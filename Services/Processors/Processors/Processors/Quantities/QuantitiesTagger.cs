namespace ProcessingTools.Processors.Processors.Quantities
{
    using Contracts;
    using Contracts.Processors.Quantities;
    using Contracts.Providers;
    using Generics;
    using ProcessingTools.Data.Miners.Contracts.Miners.Quantities;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class QuantitiesTagger : GenericStringMinerTagger<IQuantitiesDataMiner, IQuantityTagModelProvider>, IQuantitiesTagger
    {
        public QuantitiesTagger(IGenericStringDataMinerEvaluator<IQuantitiesDataMiner> evaluator, IStringTagger tagger, IQuantityTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
