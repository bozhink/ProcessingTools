namespace ProcessingTools.Mediatypes.Data.Common.Contracts.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IMediatypesRepository
    {
        Task<IEnumerable<IMediatype>> GetByFileExtension(string fileExtension);
    }
}
