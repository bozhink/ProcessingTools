// <copyright file="CatalogueOfLifeDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services;
using ProcessingTools.Contracts.Services.Bio.Taxonomy;

namespace ProcessingTools.Clients.Bio.Taxonomy.CatalogueOfLife
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife.Xml;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Implementations of some of the Catalogue Of Life (CoL) API-s.
    /// </summary>
    public class CatalogueOfLifeDataRequester : ICatalogueOfLifeDataRequester
    {
        private const string CatalogueOfLifeBaseAddress = "http://www.catalogueoflife.org";
        private readonly IHttpRequester httpRequester;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogueOfLifeDataRequester"/> class.
        /// </summary>
        /// <param name="httpRequester">HTTP requester.</param>
        public CatalogueOfLifeDataRequester(IHttpRequester httpRequester)
        {
            this.httpRequester = httpRequester ?? throw new ArgumentNullException(nameof(httpRequester));
        }

        /// <summary>
        /// Search scientific name in Catalogue Of Life (CoL).
        /// </summary>
        /// <param name="scientificName">Scientific name of the taxon which rank is searched.</param>
        /// <returns>XmlDocument of the CoL API response.</returns>
        // Example: http://www.catalogueoflife.org/col/webservice?name=Tara+spinosa&amp;response=full
        public async Task<XmlDocument> RequestXmlFromCatalogueOfLife(string scientificName)
        {
            string relativeUri = $"col/webservice?name={scientificName}&response=full";

            Uri requestUri = UriExtensions.Append(CatalogueOfLifeBaseAddress, relativeUri);

            string response = await this.httpRequester.GetStringAsync(requestUri, ContentTypes.Xml).ConfigureAwait(false);

            return response.ToXmlDocument();
        }

        /// <summary>
        /// Search scientific name in Catalogue Of Life (CoL).
        /// </summary>
        /// <param name="content">Scientific name of the taxon which rank is searched.</param>
        /// <returns>CatalogueOfLifeApiServiceResponse of the CoL API response.</returns>
        // Example: http://www.catalogueoflife.org/col/webservice?name=Tara+spinosa&amp;esponse=full
        public async Task<CatalogueOfLifeApiServiceResponseModel> RequestDataAsync(string content)
        {
            string requestName = content.UrlEncode();
            string relativeUri = $"/col/webservice?name={requestName}&response=full";

            Uri requestUri = UriExtensions.Append(CatalogueOfLifeBaseAddress, relativeUri);

            var result = await this.httpRequester.GetXmlToObjectAsync<CatalogueOfLifeApiServiceResponseModel>(requestUri).ConfigureAwait(false);
            return result;
        }
    }
}
