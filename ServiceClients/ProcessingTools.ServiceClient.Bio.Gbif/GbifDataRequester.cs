namespace ProcessingTools.ServiceClient.Bio.Gbif
{
    using System.Threading.Tasks;
    using Infrastructure.Net;
    using Infrastructure.Serialization.Json;
    using Models;

    public class GbifDataRequester
    {
        public static async Task<GbifResult> SearchGbif(string scientificName)
        {
            string url = $"http://api.gbif.org/v0.9/species/match?verbose=true&name={scientificName}";
            try
            {
                string response = await Connector.GetStringAsync(url);
                return JsonSerializer.Serialize<GbifResult>(response);
            }
            catch
            {
                throw;
            }
        }
    }
}
