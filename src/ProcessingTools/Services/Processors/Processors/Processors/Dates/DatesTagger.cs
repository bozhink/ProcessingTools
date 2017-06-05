namespace ProcessingTools.Processors.Processors.Dates
{
    using ProcessingTools.Data.Miners.Contracts.Miners.Dates;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Contracts.Processors.Dates;
    using ProcessingTools.Processors.Contracts.Providers;
    using ProcessingTools.Processors.Generics;

    public class DatesTagger : GenericStringMinerTagger<IDatesDataMiner, IDateTagModelProvider>, IDatesTagger
    {
        public DatesTagger(IGenericStringDataMinerEvaluator<IDatesDataMiner> evaluator, IStringTagger tagger, IDateTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
