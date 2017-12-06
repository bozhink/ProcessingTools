namespace ProcessingTools.Processors.Processors.Geo
{
    using ProcessingTools.Contracts.Processors;
    using ProcessingTools.Contracts.Processors.Processors.Geo;
    using ProcessingTools.Contracts.Processors.Providers;
    using ProcessingTools.Data.Miners.Contracts.Miners.Geo;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Processors.Generics;

    public class GeographicDeviationsTagger : GenericStringMinerTagger<IGeographicDeviationsDataMiner, IGeographicDeviationTagModelProvider>, IGeographicDeviationsTagger
    {
        public GeographicDeviationsTagger(IGenericStringDataMinerEvaluator<IGeographicDeviationsDataMiner> evaluator, IStringTagger tagger, IGeographicDeviationTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
