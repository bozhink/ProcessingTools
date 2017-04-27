namespace ProcessingTools.Processors.Processors.Coordinates
{
    using Contracts;
    using Contracts.Processors.Coordinates;
    using Contracts.Providers;
    using Generics;
    using ProcessingTools.Data.Miners.Contracts.Miners.Geo;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class CoordinatesTagger : GenericStringMinerTagger<ICoordinatesDataMiner, ICoordinateTagModelProvider>, ICoordinatesTagger
    {
        public CoordinatesTagger(IGenericStringDataMinerEvaluator<ICoordinatesDataMiner> evaluator, IStringTagger tagger, ICoordinateTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
