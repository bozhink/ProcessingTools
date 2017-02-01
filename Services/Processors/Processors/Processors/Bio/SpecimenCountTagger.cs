namespace ProcessingTools.Processors.Processors.Bio
{
    using Contracts;
    using Contracts.Processors.Bio;
    using Contracts.Providers;
    using Generics;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class SpecimenCountTagger : GenericStringMinerTagger<ISpecimenCountDataMiner, ISpecimenCountTagModelProvider>, ISpecimenCountTagger
    {
        public SpecimenCountTagger(IGenericStringDataMinerEvaluator<ISpecimenCountDataMiner> evaluator, IStringTagger tagger, ISpecimenCountTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
