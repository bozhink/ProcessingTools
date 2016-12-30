namespace ProcessingTools.Services.Data.Contracts.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Contracts;

    public interface ITaxonRankDataService
    {
        Task<object> Add(params ITaxonRank[] taxa);

        Task<object> Delete(params ITaxonRank[] taxa);

        Task<IEnumerable<ITaxonRank>> SearchByName(string name);

        Task<object> Update(params ITaxonRank[] taxa);
    }
}
