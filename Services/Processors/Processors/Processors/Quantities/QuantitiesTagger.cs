using ProcessingTools.Data.Miners.Contracts.Miners.Quantities;
using ProcessingTools.Layout.Processors.Contracts.Taggers;
using ProcessingTools.Processors.Contracts;
using ProcessingTools.Processors.Contracts.Providers;
using ProcessingTools.Processors.Contracts.Quantities;
using ProcessingTools.Processors.Generics;

namespace ProcessingTools.Processors.Processors.Quantities
{
    public class QuantitiesTagger : GenericStringMinerTagger<IQuantitiesDataMiner, IQuantityTagModelProvider>, IQuantitiesTagger
    {
        public QuantitiesTagger(IGenericStringDataMinerEvaluator<IQuantitiesDataMiner> evaluator, IStringTagger tagger, IQuantityTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
