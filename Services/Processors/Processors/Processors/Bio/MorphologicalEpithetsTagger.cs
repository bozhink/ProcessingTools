namespace ProcessingTools.Processors.Processors.Bio
{
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Contracts.Bio;
    using ProcessingTools.Processors.Contracts.Providers;
    using ProcessingTools.Processors.Generics;

    public class MorphologicalEpithetsTagger : GenericStringMinerTagger<IMorphologicalEpithetsDataMiner, IMorphologicalEpithetTagModelProvider>, IMorphologicalEpithetsTagger
    {
        public MorphologicalEpithetsTagger(IGenericStringDataMinerEvaluator<IMorphologicalEpithetsDataMiner> evaluator, IStringTagger tagger, IMorphologicalEpithetTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
