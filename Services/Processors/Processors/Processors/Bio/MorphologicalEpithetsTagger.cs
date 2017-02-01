namespace ProcessingTools.Processors.Processors.Bio
{
    using Contracts;
    using Contracts.Processors.Bio;
    using Contracts.Providers;
    using Generics;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class MorphologicalEpithetsTagger : GenericStringMinerTagger<IMorphologicalEpithetsDataMiner, IMorphologicalEpithetTagModelProvider>, IMorphologicalEpithetsTagger
    {
        public MorphologicalEpithetsTagger(IGenericStringDataMinerEvaluator<IMorphologicalEpithetsDataMiner> evaluator, IStringTagger tagger, IMorphologicalEpithetTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
