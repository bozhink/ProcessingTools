namespace ProcessingTools.Contracts.Processors
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Data.Miners;

    public interface IGenericStringDataMinerEvaluator<TMiner>
        where TMiner : IStringDataMiner
    {
        Task<IEnumerable<string>> Evaluate(IDocument document);
    }
}
