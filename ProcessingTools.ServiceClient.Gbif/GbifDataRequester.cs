namespace ProcessingTools.ServiceClient.Gbif
{
    using System.Threading.Tasks;
    using SystemCommons;
    using Models;

    public class GbifDataRequester
    {
        public static async Task<GbifResult> SearchGbif(string scientificName)
        {
            string url = $"http://api.gbif.org/v0.9/species/match?verbose=true&name={scientificName}";
            try
            {
                string response = await Net.GetStringAsync(url);
                return JsonSerializer.Serialize<GbifResult>(response);
            }
            catch
            {
                throw;
            }
        }
    }
}
