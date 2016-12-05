namespace ProcessingTools.Mediatypes.Data.Common.Contracts.Repositories
{
    using System.Collections.Generic;
    using Models;

    public interface ISearchableMediatypesRepository
    {
        IEnumerable<IMediatype> GetByFileExtension(string fileExtension);
    }
}
