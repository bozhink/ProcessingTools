namespace ProcessingTools.Processors.Processors.Bio
{
    using ProcessingTools.Contracts.Processors;
    using ProcessingTools.Contracts.Processors.Processors.Bio;
    using ProcessingTools.Contracts.Processors.Providers;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Processors.Generics;

    public class TypeStatusTagger : GenericStringMinerTagger<ITypeStatusDataMiner, ITypeStatusTagModelProvider>, ITypeStatusTagger
    {
        public TypeStatusTagger(IGenericStringDataMinerEvaluator<ITypeStatusDataMiner> evaluator, IStringTagger tagger, ITypeStatusTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
