namespace ProcessingTools.Processors.Geo
{
    using Contracts;
    using Contracts.Geo;
    using Contracts.Providers;
    using Generics;
    using ProcessingTools.Data.Miners.Contracts.Miners;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class AltitudesTagger : GenericStringMinerTagger<IAltitudesDataMiner, IAltitudeTagModelProvider>, IAltitudesTagger
    {
        public AltitudesTagger(IGenericStringDataMinerEvaluator<IAltitudesDataMiner> evaluator, IStringTagger tagger, IAltitudeTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
