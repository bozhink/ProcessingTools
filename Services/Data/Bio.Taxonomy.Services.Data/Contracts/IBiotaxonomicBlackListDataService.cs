namespace ProcessingTools.Bio.Taxonomy.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IBiotaxonomicBlackListDataService
    {
        Task<object> Add(params string[] items);

        Task<object> Delete(params string[] items);
    }
}
