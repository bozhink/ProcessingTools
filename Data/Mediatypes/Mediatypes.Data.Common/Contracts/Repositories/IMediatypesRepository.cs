namespace ProcessingTools.Mediatypes.Data.Common.Contracts.Repositories
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMediatypesRepository
    {
        Task<object> Add(IMediatype mediatype);

        IEnumerable<IMediatype> GetByFileExtension(string fileExtension);

        Task<object> Remove(string fileExtension);

        Task<object> UpdateDescription(string fileExtension, string description);

        Task<long> SaveChanges();
    }
}
