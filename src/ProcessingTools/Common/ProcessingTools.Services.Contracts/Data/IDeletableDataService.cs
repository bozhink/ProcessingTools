namespace ProcessingTools.Contracts.Services.Data
{
    using System.Threading.Tasks;

    public interface IDeletableDataService<T>
    {
        Task<object> Delete(params T[] items);
    }
}
