namespace ProcessingTools.Processors.Processors.Geo
{
    using Contracts;
    using Contracts.Processors.Geo;
    using Contracts.Providers;
    using Generics;
    using ProcessingTools.Data.Miners.Contracts.Miners.Geo;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class GeographicDeviationsTagger : GenericStringMinerTagger<IGeographicDeviationsDataMiner, IGeographicDeviationTagModelProvider>, IGeographicDeviationsTagger
    {
        public GeographicDeviationsTagger(IGenericStringDataMinerEvaluator<IGeographicDeviationsDataMiner> evaluator, IStringTagger tagger, IGeographicDeviationTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
