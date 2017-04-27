namespace ProcessingTools.Processors.Processors.Institutions
{
    using Contracts;
    using Contracts.Processors.Institutions;
    using Contracts.Providers;
    using Generics;
    using ProcessingTools.Data.Miners.Contracts.Miners.Institutions;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class InstitutionsTagger : GenericStringMinerTagger<IInstitutionsDataMiner, IInstitutionTagModelProvider>, IInstitutionsTagger
    {
        public InstitutionsTagger(IGenericStringDataMinerEvaluator<IInstitutionsDataMiner> evaluator, IStringTagger tagger, IInstitutionTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
