using ProcessingTools.Data.Miners.Contracts.Miners.Geo;
using ProcessingTools.Layout.Processors.Contracts.Taggers;
using ProcessingTools.Processors.Contracts;
using ProcessingTools.Processors.Contracts.Geo;
using ProcessingTools.Processors.Contracts.Providers;
using ProcessingTools.Processors.Generics;

namespace ProcessingTools.Processors.Processors.Geo
{
    public class AltitudesTagger : GenericStringMinerTagger<IAltitudesDataMiner, IAltitudeTagModelProvider>, IAltitudesTagger
    {
        public AltitudesTagger(IGenericStringDataMinerEvaluator<IAltitudesDataMiner> evaluator, IStringTagger tagger, IAltitudeTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
