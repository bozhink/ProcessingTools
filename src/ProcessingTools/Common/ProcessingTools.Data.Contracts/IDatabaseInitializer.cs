namespace ProcessingTools.Contracts.Data
{
    using System.Threading.Tasks;

    public interface IDatabaseInitializer
    {
        Task<object> InitializeAsync();
    }
}
