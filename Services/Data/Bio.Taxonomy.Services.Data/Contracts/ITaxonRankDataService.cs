namespace ProcessingTools.Bio.Taxonomy.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Contracts;

    public interface ITaxonRankDataService
    {
        Task<object> Add(params ITaxonRank[] taxa);

        Task<object> Delete(params ITaxonRank[] taxa);

        Task<IEnumerable<ITaxonRank>> FindByName(string name);

        Task<IEnumerable<ITaxonRank>> GetWhiteListedTaxa();

        Task<IEnumerable<ITaxonRank>> SearchByName(string name);

        Task<object> Update(params ITaxonRank[] taxa);
    }
}
