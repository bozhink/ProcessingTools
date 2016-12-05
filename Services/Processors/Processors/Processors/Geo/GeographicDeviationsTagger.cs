namespace ProcessingTools.Processors.Processors.Geo
{
    using ProcessingTools.Data.Miners.Contracts.Miners.Geo;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Contracts.Geo;
    using ProcessingTools.Processors.Contracts.Providers;
    using ProcessingTools.Processors.Generics;

    public class GeographicDeviationsTagger : GenericStringMinerTagger<IGeographicDeviationsDataMiner, IGeographicDeviationTagModelProvider>, IGeographicDeviationsTagger
    {
        public GeographicDeviationsTagger(IGenericStringDataMinerEvaluator<IGeographicDeviationsDataMiner> evaluator, IStringTagger tagger, IGeographicDeviationTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
