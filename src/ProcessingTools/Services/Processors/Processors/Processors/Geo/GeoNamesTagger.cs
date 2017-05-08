﻿namespace ProcessingTools.Processors.Processors.Geo
{
    using Contracts;
    using Contracts.Processors.Geo;
    using Contracts.Providers;
    using Generics;
    using ProcessingTools.Data.Miners.Contracts.Miners.Geo;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class GeoNamesTagger : GenericStringMinerTagger<IGeoNamesDataMiner, IGeoNameTagModelProvider>, IGeoNamesTagger
    {
        public GeoNamesTagger(IGenericStringDataMinerEvaluator<IGeoNamesDataMiner> evaluator, IStringTagger tagger, IGeoNameTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}