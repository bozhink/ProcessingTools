namespace ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif
{
    using System.Threading.Tasks;
    using Infrastructure.Net;
    using Models;

    public class GbifDataRequester
    {
        private const string BaseAddress = "http://api.gbif.org";

        public static async Task<GbifApiResponseModel> SearchGbif(string scientificName)
        {
            string url = $"v0.9/species/match?verbose=true&name={scientificName}";
            try
            {
                var connector = new Connector(BaseAddress);
                var result = await connector.GetDeserializedDataContractJsonAsync<GbifApiResponseModel>(url);
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
