namespace ProcessingTools.Processors.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Common.Contracts;

    public interface IGenericStringDataMinerEvaluator<TMiner>
        where TMiner : IStringDataMiner
    {
        Task<IEnumerable<string>> Evaluate(IDocument document);
    }
}
