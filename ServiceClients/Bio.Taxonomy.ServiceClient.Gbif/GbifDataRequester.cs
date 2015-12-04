namespace ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif
{
    using System.Threading.Tasks;
    using Infrastructure.Net;
    using Infrastructure.Serialization.Json;
    using Models;

    public class GbifDataRequester
    {
        public const string BaseAddress = "http://api.gbif.org";

        public static async Task<GbifApiResponseModel> SearchGbif(string scientificName)
        {
            const string UrlFormat = "v0.9/species/match?verbose=true&name={0}";
            try
            {
                string response = await Connector.GetJsonAsync(BaseAddress, UrlFormat, scientificName);
                return JsonSerializer.Deserialize<GbifApiResponseModel>(response);
            }
            catch
            {
                throw;
            }
        }
    }
}
