namespace ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Models;

    using ProcessingTools.Net.Constants;
    using ProcessingTools.Net.Extensions;
    using ProcessingTools.Net.Factories.Contracts;
    using ProcessingTools.Xml.Extensions;

    /// <summary>
    /// Implementations of some of the Catalogue Of Life (CoL) API-s.
    /// </summary>
    public class CatalogueOfLifeDataRequester : ICatalogueOfLifeDataRequester
    {
        private const string CatalogueOfLifeBaseAddress = "http://www.catalogueoflife.org";
        private readonly INetConnectorFactory connectorFactory;

        public CatalogueOfLifeDataRequester(INetConnectorFactory connectorFactory)
        {
            if (connectorFactory == null)
            {
                throw new ArgumentNullException(nameof(connectorFactory));
            }

            this.connectorFactory = connectorFactory;
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
            string response = await connector.Get(url, ContentTypeConstants.XmlContentType);
            return response.ToXmlDocument();
        }

        /// <summary>
        /// Search scientific name in Catalogue Of Life (CoL).
        /// </summary>
        /// <param name="scientificName">Scientific name of the taxon which rank is searched.</param>
        /// <returns>CatalogueOfLifeApiServiceResponse of the CoL API response.</returns>
        /// <example>http://www.catalogueoflife.org/col/webservice?name=Tara+spinosa&response=full</example>
        public async Task<CatalogueOfLifeApiServiceResponse> RequestData(string scientificName)
        {
            string requestName = scientificName.UrlEncode();
            string url = $"/col/webservice?name={requestName}&response=full";

            var connector = this.connectorFactory.Create(CatalogueOfLifeBaseAddress);
            var result = await connector.GetAndDeserializeXml<CatalogueOfLifeApiServiceResponse>(url);
            return result;
        }
    }
}