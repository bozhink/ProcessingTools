using ProcessingTools.Data.Miners.Contracts.Miners.Institutions;
using ProcessingTools.Layout.Processors.Contracts.Taggers;
using ProcessingTools.Processors.Contracts;
using ProcessingTools.Processors.Contracts.Institutions;
using ProcessingTools.Processors.Contracts.Providers;
using ProcessingTools.Processors.Generics;

namespace ProcessingTools.Processors.Processors.Institutions
{
    public class InstitutionsTagger : GenericStringMinerTagger<IInstitutionsDataMiner, IInstitutionTagModelProvider>, IInstitutionsTagger
    {
        public InstitutionsTagger(IGenericStringDataMinerEvaluator<IInstitutionsDataMiner> evaluator, IStringTagger tagger, IInstitutionTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
