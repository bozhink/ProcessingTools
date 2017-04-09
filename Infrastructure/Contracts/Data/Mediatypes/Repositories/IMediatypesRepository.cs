namespace ProcessingTools.Contracts.Data.Mediatypes.Repositories
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Mediatypes.Models;

    public interface IMediatypesRepository
    {
        Task<object> Add(IMediatype mediatype);

        Task<object> Remove(string fileExtension);

        Task<object> UpdateDescription(string fileExtension, string description);

        Task<long> SaveChanges();
    }
}
