// <copyright file="CatalogueOfLifeDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Bio.Taxonomy.CatalogueOfLife
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife.Xml;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Clients.Bio.Taxonomy;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Implementations of some of the Catalogue Of Life (CoL) API-s.
    /// </summary>
    public class CatalogueOfLifeDataRequester : ICatalogueOfLifeDataRequester
    {
        private const string CatalogueOfLifeBaseAddress = "http://www.catalogueoflife.org";
        private readonly INetConnectorFactory connectorFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogueOfLifeDataRequester"/> class.
        /// </summary>
        /// <param name="connectorFactory">Net connector factory.</param>
        public CatalogueOfLifeDataRequester(INetConnectorFactory connectorFactory)
        {
            this.connectorFactory = connectorFactory ?? throw new ArgumentNullException(nameof(connectorFactory));
        }

        /// <summary>
        /// Search scientific name in Catalogue Of Life (CoL).
        /// </summary>
        /// <param name="scientificName">Scientific name of the taxon which rank is searched.</param>
        /// <returns>XmlDocument of the CoL API response.</returns>
        /// <example>http://www.catalogueoflife.org/col/webservice?name=Tara+spinosa&amp;response=full</example>
        public async Task<XmlDocument> RequestXmlFromCatalogueOfLife(string scientificName)
        {
            string url = $"col/webservice?name={scientificName}&response=full";

            var connector = this.connectorFactory.Create(CatalogueOfLifeBaseAddress);
            string response = await connector.GetAsync(url, ContentTypes.Xml).ConfigureAwait(false);
            return response.ToXmlDocument();
        }

        /// <summary>
        /// Search scientific name in Catalogue Of Life (CoL).
        /// </summary>
        /// <param name="content">Scientific name of the taxon which rank is searched.</param>
        /// <returns>CatalogueOfLifeApiServiceResponse of the CoL API response.</returns>
        /// <example>http://www.catalogueoflife.org/col/webservice?name=Tara+spinosa&amp;esponse=full</example>
        public async Task<CatalogueOfLifeApiServiceResponseModel> RequestDataAsync(string content)
        {
            string requestName = content.UrlEncode();
            string url = $"/col/webservice?name={requestName}&response=full";

            var connector = this.connectorFactory.Create(CatalogueOfLifeBaseAddress);
            var result = await connector.GetXmlObjectAsync<CatalogueOfLifeApiServiceResponseModel>(url).ConfigureAwait(false);
            return result;
        }
    }
}
