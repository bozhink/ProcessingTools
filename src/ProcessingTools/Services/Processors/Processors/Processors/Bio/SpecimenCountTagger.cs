namespace ProcessingTools.Processors.Processors.Bio
{
    using ProcessingTools.Contracts.Processors;
    using ProcessingTools.Contracts.Processors.Processors.Bio;
    using ProcessingTools.Contracts.Processors.Providers;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Processors.Generics;

    public class SpecimenCountTagger : GenericStringMinerTagger<ISpecimenCountDataMiner, ISpecimenCountTagModelProvider>, ISpecimenCountTagger
    {
        public SpecimenCountTagger(IGenericStringDataMinerEvaluator<ISpecimenCountDataMiner> evaluator, IStringTagger tagger, ISpecimenCountTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
