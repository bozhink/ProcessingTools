namespace ProcessingTools.Mediatype.Services.Data.Contracts
{
    using Models;

    public interface IMediatypeService
    {
        MediatypeDataServiceResponseModel GetMediatype(string fileExtension);
    }
}