namespace ProcessingTools.Services.Data.Contracts.Bio.Taxonomy
{
    using System.Threading.Tasks;

    public interface IBiotaxonomicBlackListDataService
    {
        Task<object> Add(params string[] items);

        Task<object> Delete(params string[] items);
    }
}
