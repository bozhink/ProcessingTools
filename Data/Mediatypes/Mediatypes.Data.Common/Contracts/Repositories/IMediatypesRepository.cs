namespace ProcessingTools.Mediatypes.Data.Common.Contracts.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IMediatypesRepository
    {
        Task<object> Add(IMediatype mediatype);

        Task<object> Remove(string fileExtension);

        Task<object> UpdateDescription(string fileExtension, string description);

        Task<long> SaveChanges();
    }
}
