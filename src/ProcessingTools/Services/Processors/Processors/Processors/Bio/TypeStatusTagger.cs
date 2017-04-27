namespace ProcessingTools.Processors.Processors.Bio
{
    using Contracts;
    using Contracts.Processors.Bio;
    using Contracts.Providers;
    using Generics;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class TypeStatusTagger : GenericStringMinerTagger<ITypeStatusDataMiner, ITypeStatusTagModelProvider>, ITypeStatusTagger
    {
        public TypeStatusTagger(IGenericStringDataMinerEvaluator<ITypeStatusDataMiner> evaluator, IStringTagger tagger, ITypeStatusTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
