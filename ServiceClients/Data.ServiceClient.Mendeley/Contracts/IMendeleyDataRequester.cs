namespace ProcessingTools.Data.ServiceClient.Mendeley.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IMendeleyDataRequester
    {
        Task<IEnumerable<CatalogResponseModel>> GetDocumentInformationByDoi(string doi);
    }
}
