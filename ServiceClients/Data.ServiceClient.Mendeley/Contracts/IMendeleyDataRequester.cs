namespace ProcessingTools.Data.ServiceClient.Mendeley.Contracts
{
    using System.Threading.Tasks;

    public interface IMendeleyDataRequester
    {
        Task GetDocumentInformationByDoi(string doi);
    }
}
