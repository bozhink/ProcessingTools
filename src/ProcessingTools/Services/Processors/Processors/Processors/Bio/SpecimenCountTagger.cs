namespace ProcessingTools.Processors.Processors.Bio
{
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio;
    using ProcessingTools.Processors.Abstractions;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Contracts.Bio;
    using ProcessingTools.Processors.Models.Contracts;

    public class SpecimenCountTagger : StringDataMinerTagger<ISpecimenCountDataMiner, ISpecimenCountTagModelProvider>, ISpecimenCountTagger
    {
        public SpecimenCountTagger(IStringDataMinerEvaluator<ISpecimenCountDataMiner> evaluator, IStringTagger tagger, ISpecimenCountTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
