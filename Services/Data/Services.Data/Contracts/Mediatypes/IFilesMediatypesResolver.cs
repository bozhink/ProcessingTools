namespace ProcessingTools.Services.Data.Contracts.Mediatypes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Mediatypes;

    public interface IFilesMediatypesResolver
    {
        Task<IEnumerable<IFileWithMediatype>> Resolve(params string[] fileNames);
    }
}
