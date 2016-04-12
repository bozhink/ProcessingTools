namespace ProcessingTools.Data.ServiceClient.Mendeley
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Contracts;
    using Models;

    public class MendeleyDataRequester : IMendeleyDataRequester
    {
        private const string MendeleyApiBaseAddress = "https://api.mendeley.com";

        /// <summary>
        /// Get information about a document by its DOI.
        /// </summary>
        /// <param name="doi">DOI to search.</param>
        /// <returns></returns>
        /// <remarks>
        /// See http://dev.mendeley.com/methods/#catalog-document-views
        /// </remarks>
        public async Task<IEnumerable<CatalogResponseModel>> GetDocumentInformationByDoi(string doi)
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
                return null;
            }
        }
    }
}
