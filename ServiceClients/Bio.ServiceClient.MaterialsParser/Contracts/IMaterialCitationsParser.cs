namespace ProcessingTools.Bio.ServiceClient.MaterialsParser.Contracts
{
    using System.Threading.Tasks;

    public interface IMaterialCitationsParser
    {
        Task<string> Invoke(string citations);
    }
}
