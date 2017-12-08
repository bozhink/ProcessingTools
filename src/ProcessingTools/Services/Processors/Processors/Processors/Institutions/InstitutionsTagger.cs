namespace ProcessingTools.Processors.Processors.Institutions
{
    using ProcessingTools.Contracts.Processors;
    using ProcessingTools.Contracts.Processors.Processors.Institutions;
    using ProcessingTools.Contracts.Processors.Providers;
    using ProcessingTools.Data.Miners.Contracts.Miners.Institutions;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Processors.Generics;

    public class InstitutionsTagger : GenericStringMinerTagger<IInstitutionsDataMiner, IInstitutionTagModelProvider>, IInstitutionsTagger
    {
        public InstitutionsTagger(IGenericStringDataMinerEvaluator<IInstitutionsDataMiner> evaluator, IStringTagger tagger, IInstitutionTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
