using ProcessingTools.Data.Miners.Contracts.Miners.Geo;
using ProcessingTools.Layout.Processors.Contracts.Taggers;
using ProcessingTools.Processors.Contracts;
using ProcessingTools.Processors.Contracts.Coordinates;
using ProcessingTools.Processors.Contracts.Providers;
using ProcessingTools.Processors.Generics;

namespace ProcessingTools.Processors.Processors.Coordinates
{
    public class CoordinatesTagger : GenericStringMinerTagger<ICoordinatesDataMiner, ICoordinateTagModelProvider>, ICoordinatesTagger
    {
        public CoordinatesTagger(IGenericStringDataMinerEvaluator<ICoordinatesDataMiner> evaluator, IStringTagger tagger, ICoordinateTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
