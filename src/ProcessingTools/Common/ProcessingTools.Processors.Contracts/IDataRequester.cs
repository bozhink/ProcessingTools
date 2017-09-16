namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    public interface IDataRequester<T>
    {
        Task<T> RequestDataAsync(string content);
    }
}