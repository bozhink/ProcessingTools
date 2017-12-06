namespace ProcessingTools.Processors.Processors.Geo
{
    using ProcessingTools.Contracts.Processors;
    using ProcessingTools.Contracts.Processors.Processors.Geo;
    using ProcessingTools.Contracts.Processors.Providers;
    using ProcessingTools.Data.Miners.Contracts.Miners.Geo;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Processors.Generics;

    public class GeoNamesTagger : GenericStringMinerTagger<IGeoNamesDataMiner, IGeoNameTagModelProvider>, IGeoNamesTagger
    {
        public GeoNamesTagger(IGenericStringDataMinerEvaluator<IGeoNamesDataMiner> evaluator, IStringTagger tagger, IGeoNameTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
