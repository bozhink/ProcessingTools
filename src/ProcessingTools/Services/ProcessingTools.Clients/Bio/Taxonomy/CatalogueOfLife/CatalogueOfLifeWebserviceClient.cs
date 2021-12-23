// <copyright file="CatalogueOfLifeWebserviceClient.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Bio.Taxonomy.CatalogueOfLife
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Serialization;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Catalogue of Life (CoL) webservice client.
    /// </summary>
    public class CatalogueOfLifeWebserviceClient : ICatalogueOfLifeWebserviceClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IXmlDeserializer<CatalogueOfLifeApiServiceXmlResponseModel> deserializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogueOfLifeWebserviceClient"/> class.
        /// </summary>
        /// <param name="httpClientFactory">Instance of <see cref="IHttpClientFactory"/>.</param>
        /// <param name="deserializer">Instance of <see cref="IXmlDeserializer{CatalogueOfLifeApiServiceXmlResponseModel}"/>.</param>
        public CatalogueOfLifeWebserviceClient(IHttpClientFactory httpClientFactory, IXmlDeserializer<CatalogueOfLifeApiServiceXmlResponseModel> deserializer)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            this.deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
        }

        /// <summary>
        /// Search scientific name in Catalogue Of Life (CoL).
        /// </summary>
        /// <param name="scientificName">Scientific name of the taxon which rank is searched.</param>
        /// <returns>XmlDocument of the CoL API response.</returns>
        // Example: http://www.catalogueoflife.org/col/webservice?name=Tara+spinosa&amp;response=full
        public async Task<XmlDocument> RequestXmlFromCatalogueOfLife(string scientificName)
        {
            IDictionary<string, string> queryParameters = new Dictionary<string, string>
            {
                { "name", scientificName },
                { "response", "full" },
            };

            string queryString = await queryParameters.GetQueryStringAsync().ConfigureAwait(false);

            string relativeUri = $"col/webservice?{queryString}";

            var client = this.httpClientFactory.CreateClient(nameof(CatalogueOfLifeWebserviceClient));

            string response = await client.GetStringAsync(relativeUri).ConfigureAwait(false);

            return response.ToXmlDocument();
        }

        /// <inheritdoc/>
        // Example: http://www.catalogueoflife.org/col/webservice?name=Tara+spinosa&amp;esponse=full
        public async Task<CatalogueOfLifeApiServiceXmlResponseModel> GetDataPerNameAsync(string name)
        {
            IDictionary<string, string> queryParameters = new Dictionary<string, string>
            {
                { "name", name },
                { "response", "full" },
            };

            string queryString = await queryParameters.GetQueryStringAsync().ConfigureAwait(false);

            string relativeUri = $"col/webservice?{queryString}";

            var client = this.httpClientFactory.CreateClient(nameof(CatalogueOfLifeWebserviceClient));

            var stream = await client.GetStreamAsync(relativeUri).ConfigureAwait(false);

            var result = this.deserializer.Deserialize(stream);

            return result;
        }
    }
}
