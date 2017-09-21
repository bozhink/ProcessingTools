namespace ProcessingTools.Contracts.Data.Mediatypes.Repositories
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Data.Mediatypes.Models;

    public interface ISearchableMediatypesRepository
    {
        IEnumerable<IMediatype> GetByFileExtension(string fileExtension);
    }
}
