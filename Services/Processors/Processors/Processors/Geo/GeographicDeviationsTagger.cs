namespace ProcessingTools.Processors.Geo
{
    using Contracts;
    using Contracts.Geo;
    using Contracts.Providers;
    using Generics;
    using ProcessingTools.Data.Miners.Contracts.Miners;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class GeographicDeviationsTagger : GenericStringMinerTagger<IGeographicDeviationsDataMiner, IGeographicDeviationTagModelProvider>, IGeographicDeviationsTagger
    {
        public GeographicDeviationsTagger(IGenericStringDataMinerEvaluator<IGeographicDeviationsDataMiner> evaluator, IStringTagger tagger, IGeographicDeviationTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
