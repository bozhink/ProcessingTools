namespace ProcessingTools.Processors.Processors.Bio
{
    using ProcessingTools.Data.Miners.Contracts.Bio;
    using ProcessingTools.Processors.Abstractions;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Contracts.Bio;
    using ProcessingTools.Processors.Models.Contracts;

    public class TypeStatusTagger : StringDataMinerTagger<ITypeStatusDataMiner, ITypeStatusTagModelProvider>, ITypeStatusTagger
    {
        public TypeStatusTagger(IStringDataMinerEvaluator<ITypeStatusDataMiner> evaluator, IStringTagger tagger, ITypeStatusTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
