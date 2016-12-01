namespace ProcessingTools.Processors.Dates
{
    using Contracts;
    using Contracts.Dates;
    using Contracts.Providers;
    using Generics;
    using ProcessingTools.Data.Miners.Contracts.Miners;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class DatesTagger : GenericStringMinerTagger<IDatesDataMiner, IDateTagModelProvider>, IDatesTagger
    {
        public DatesTagger(IGenericStringDataMinerEvaluator<IDatesDataMiner> evaluator, IStringTagger tagger, IDateTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
