namespace ProcessingTools.Mediatype.Services.Data.Contracts
{
    using System.Linq;
    using Models;

    public interface IMediatypeService
    {
        IQueryable<MediatypeDataServiceResponseModel> GetMediatype(string fileExtension);
    }
}