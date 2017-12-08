// <copyright file="MendeleyDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.References.Mendeley
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using ProcessingTools.Clients.Models.References.Mendeley;
    using ProcessingTools.Contracts.Clients.References;

    /// <summary>
    /// Mendeley data requester.
    /// </summary>
    public class MendeleyDataRequester : IMendeleyDataRequester
    {
        private const string MendeleyApiBaseAddress = "https://api.mendeley.com";

        /// <summary>
        /// Get information about a document by its DOI.
        /// </summary>
        /// <param name="doi">DOI to search.</param>
        /// <returns>Catalog data</returns>
        /// <remarks>
        /// See http://dev.mendeley.com/methods/#catalog-document-views
        /// </remarks>
        public async Task<CatalogResponseModel[]> GetDocumentInformationByDoi(string doi)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.mendeley-document.1+json"));

                client.BaseAddress = new Uri(MendeleyApiBaseAddress);

                string url = $"/catalog?doi={doi}";
                var stream = await client.GetStreamAsync(url).ConfigureAwait(false);
                if (stream != null && stream.CanRead)
                {
                    // TODO: serialization model
                    //// ...
                }

                return null;
            }
        }
    }
}
