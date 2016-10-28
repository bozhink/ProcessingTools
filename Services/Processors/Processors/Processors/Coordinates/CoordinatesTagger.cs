namespace ProcessingTools.Processors.Coordinates
{
    using Contracts;
    using Contracts.Coordinates;
    using Contracts.Providers;
    using Generics;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Geo.Data.Miners.Contracts;

    public class CoordinatesTagger : GenericStringMinerTagger<ICoordinatesDataMiner, ICoordinateTagModelProvider>, ICoordinatesTagger
    {
        public CoordinatesTagger(IGenericStringDataMinerEvaluator<ICoordinatesDataMiner> evaluator, IStringTagger tagger, ICoordinateTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
