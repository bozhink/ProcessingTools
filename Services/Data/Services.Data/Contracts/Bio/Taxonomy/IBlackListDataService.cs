namespace ProcessingTools.Services.Data.Contracts.Bio.Taxonomy
{
    using System.Threading.Tasks;

    public interface IBlackListDataService
    {
        Task<object> Delete(params string[] items);

        Task<object> Upsert(params string[] items);
    }
}
