namespace ProcessingTools.Journals.Services.Data.Contracts.Services
{
    using System.Threading.Tasks;

    public interface IPublishersDataService
    {
        Task<object> Add();
    }
}
