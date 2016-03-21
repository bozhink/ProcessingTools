namespace ProcessingTools.Data.ServiceClient.Mendeley
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Contracts;

    public class MendeleyDataRequester : IMendeleyDataRequester
    {
        private const string MendeleyApiBaseAddress = "https://api.mendeley.com";

        public async Task GetDocumentInformationByDoi(string doi)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.mendeley-document.1+json"));

                client.BaseAddress = new Uri(MendeleyApiBaseAddress);

                string url = $"/catalog?doi={doi}";
                var stream = await client.GetStreamAsync(url);

                // TODO: serialization model
                //// ...
            }
        }
    }
}
