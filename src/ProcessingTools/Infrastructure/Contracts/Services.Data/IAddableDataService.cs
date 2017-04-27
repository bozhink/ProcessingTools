namespace ProcessingTools.Contracts.Services.Data
{
    using System.Threading.Tasks;

    public interface IAddableDataService<T>
    {
        Task<object> Add(params T[] items);
    }
}
