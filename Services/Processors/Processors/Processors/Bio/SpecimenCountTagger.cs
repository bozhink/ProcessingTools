namespace ProcessingTools.Processors.Bio
{
    using Contracts;
    using Contracts.Bio;
    using Contracts.Providers;
    using Generics;
    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class SpecimenCountTagger : GenericStringMinerTagger<ISpecimenCountDataMiner, ISpecimenCountTagModelProvider>, ISpecimenCountTagger
    {
        public SpecimenCountTagger(IGenericStringDataMinerEvaluator<ISpecimenCountDataMiner> evaluator, IStringTagger tagger, ISpecimenCountTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
