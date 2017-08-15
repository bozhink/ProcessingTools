namespace ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Contracts;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Models;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Net;

    /// <summary>
    /// Implementations of some of the Catalogue Of Life (CoL) API-s.
    /// </summary>
    public class CatalogueOfLifeDataRequester : ICatalogueOfLifeDataRequester
    {
        private const string CatalogueOfLifeBaseAddress = "http://www.catalogueoflife.org";
        private readonly INetConnectorFactory connectorFactory;

        public CatalogueOfLifeDataRequester(INetConnectorFactory connectorFactory)
        {
            this.connectorFactory = connectorFactory ?? throw new ArgumentNullException(nameof(connectorFactory));
        }

        /// <summary>
        /// Search scientific name in Catalogue Of Life (CoL).
        /// </summary>
        /// <param name="scientificName">Scientific name of the taxon which rank is searched.</param>
        /// <returns>XmlDocument of the CoL API response.</returns>
        /// <example>http://www.catalogueoflife.org/col/webservice?name=Tara+spinosa&response=full</example>
        public async Task<XmlDocument> RequestXmlFromCatalogueOfLife(string scientificName)
        {
            string url = $"col/webservice?name={scientificName}&response=full";

            var connector = this.connectorFactory.Create(CatalogueOfLifeBaseAddress);
            string response = await connector.GetAsync(url, ContentTypes.Xml);
            return response.ToXmlDocument();
        }

        /// <summary>
        /// Search scientific name in Catalogue Of Life (CoL).
        /// </summary>
        /// <param name="content">Scientific name of the taxon which rank is searched.</param>
        /// <returns>CatalogueOfLifeApiServiceResponse of the CoL API response.</returns>
        /// <example>http://www.catalogueoflife.org/col/webservice?name=Tara+spinosa&response=full</example>
        public async Task<CatalogueOfLifeApiServiceResponse> RequestData(string content)
        {
            string requestName = content.UrlEncode();
            string url = $"/col/webservice?name={requestName}&response=full";

            var connector = this.connectorFactory.Create(CatalogueOfLifeBaseAddress);
            var result = await connector.GetXmlObjectAsync<CatalogueOfLifeApiServiceResponse>(url);
            return result;
        }
    }
}
