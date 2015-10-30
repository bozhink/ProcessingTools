namespace ProcessingTools.ServiceClient.Gbif
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using SystemCommons;
    using Models;

    public class GbifDataRequester
    {
        public static async Task<GbifResult> SearchGbif(string scientificName)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetStringAsync("http://api.gbif.org/v0.9/species/match?verbose=true&name=" + scientificName);

                    return JsonSerializer.Serialize<GbifResult>(response);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
