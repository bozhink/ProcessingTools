namespace ProcessingTools.Processors.Processors.Geo
{
    using Contracts;
    using Contracts.Processors.Geo;
    using Contracts.Providers;
    using Generics;
    using ProcessingTools.Data.Miners.Contracts.Miners.Geo;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class GeoEpithetsTagger : GenericStringMinerTagger<IGeoEpithetsDataMiner, IGeoEpithetTagModelProvider>, IGeoEpithetsTagger
    {
        public GeoEpithetsTagger(IGenericStringDataMinerEvaluator<IGeoEpithetsDataMiner> evaluator, IStringTagger tagger, IGeoEpithetTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
