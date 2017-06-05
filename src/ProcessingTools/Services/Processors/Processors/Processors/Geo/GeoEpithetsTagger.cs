namespace ProcessingTools.Processors.Processors.Geo
{
    using ProcessingTools.Data.Miners.Contracts.Miners.Geo;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Contracts.Processors.Geo;
    using ProcessingTools.Processors.Contracts.Providers;
    using ProcessingTools.Processors.Generics;

    public class GeoEpithetsTagger : GenericStringMinerTagger<IGeoEpithetsDataMiner, IGeoEpithetTagModelProvider>, IGeoEpithetsTagger
    {
        public GeoEpithetsTagger(IGenericStringDataMinerEvaluator<IGeoEpithetsDataMiner> evaluator, IStringTagger tagger, IGeoEpithetTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
