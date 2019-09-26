// <copyright file="CatalogueOfLifeDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Bio.Taxonomy.CatalogueOfLife
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Implementations of some of the Catalogue Of Life (CoL) API-s.
    /// </summary>
    public class CatalogueOfLifeDataRequester : ICatalogueOfLifeDataRequester
    {
        private readonly IHttpClientFactory httpClientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogueOfLifeDataRequester"/> class.
        /// </summary>
        /// <param name="httpClientFactory">Instance of <see cref="IHttpClientFactory"/>.</param>
        public CatalogueOfLifeDataRequester(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        /// <summary>
        /// Search scientific name in Catalogue Of Life (CoL).
        /// </summary>
        /// <param name="scientificName">Scientific name of the taxon which rank is searched.</param>
        /// <returns>XmlDocument of the CoL API response.</returns>
        // Example: http://www.catalogueoflife.org/col/webservice?name=Tara+spinosa&amp;response=full
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "HttpClient")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "HttpClient")]
        public async Task<XmlDocument> RequestXmlFromCatalogueOfLife(string scientificName)
        {
            IDictionary<string, string> queryParameters = new Dictionary<string, string>
            {
                { "name", scientificName },
                { "response", "full" },
            };

            string queryString = await queryParameters.GetQueryStringAsync().ConfigureAwait(false);

            string relativeUri = $"col/webservice?{queryString}";

            var client = this.httpClientFactory.CreateClient();

            string response = await client.GetStringAsync(relativeUri).ConfigureAwait(false);

            return response.ToXmlDocument();
        }

        /// <summary>
        /// Search scientific name in Catalogue Of Life (CoL).
        /// </summary>
        /// <param name="content">Scientific name of the taxon which rank is searched.</param>
        /// <returns>CatalogueOfLifeApiServiceResponse of the CoL API response.</returns>
        // Example: http://www.catalogueoflife.org/col/webservice?name=Tara+spinosa&amp;esponse=full
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "HttpClient")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "HttpClient")]
        public async Task<CatalogueOfLifeApiServiceXmlResponseModel> RequestDataAsync(string content)
        {
            IDictionary<string, string> queryParameters = new Dictionary<string, string>
            {
                { "name", content },
                { "response", "full" },
            };

            string queryString = await queryParameters.GetQueryStringAsync().ConfigureAwait(false);

            string relativeUri = $"col/webservice?{queryString}";

            var client = this.httpClientFactory.CreateClient(nameof(CatalogueOfLifeDataRequester));

            ////string resp = await client.GetStringAsync(relativeUri).ConfigureAwait(false);

            var stream = await client.GetStreamAsync(relativeUri).ConfigureAwait(false);

            var reader = XmlReader.Create(stream, new XmlReaderSettings
            {
                ValidationType = ValidationType.None,
                DtdProcessing = DtdProcessing.Ignore,
                CloseInput = true,
                IgnoreComments = true,
                IgnoreProcessingInstructions = true,
            });

            var serializer = new XmlSerializer(typeof(CatalogueOfLifeApiServiceXmlResponseModel));

            var result = (CatalogueOfLifeApiServiceXmlResponseModel)serializer.Deserialize(reader);

            return result;
        }
    }
}
