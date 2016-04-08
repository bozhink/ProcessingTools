namespace ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif
{
    using System.Threading.Tasks;

    using Contracts;
    using Infrastructure.Net;
    using Models;

    public class GbifDataRequester : IGbifDataRequester
    {
        private const string BaseAddress = "http://api.gbif.org";

        public async Task<GbifApiResponseModel> RequestData(string scientificName)
        {
            string url = $"v0.9/species/match?verbose=true&name={scientificName}";
            try
            {
                var connector = new Connector(BaseAddress);
                var result = await connector.GetAndDeserializeDataContractJson<GbifApiResponseModel>(url);
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
