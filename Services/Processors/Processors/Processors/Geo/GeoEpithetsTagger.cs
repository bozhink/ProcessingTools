namespace ProcessingTools.Processors.Geo
{
    using Contracts;
    using Contracts.Geo;
    using Contracts.Providers;
    using Generics;
    using ProcessingTools.Geo.Data.Miners.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class GeoEpithetsTagger : GenericStringMinerTagger<IGeoEpithetsDataMiner, IGeoEpithetTagModelProvider>, IGeoEpithetsTagger
    {
        public GeoEpithetsTagger(IGenericStringDataMinerEvaluator<IGeoEpithetsDataMiner> evaluator, IStringTagger tagger, IGeoEpithetTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
