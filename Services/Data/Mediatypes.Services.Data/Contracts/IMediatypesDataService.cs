namespace ProcessingTools.Mediatypes.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IMediatypesDataService
    {
        Task<IEnumerable<IMediatypeServiceModel>> ResolveMediatype(string fileExtension);
    }
}
