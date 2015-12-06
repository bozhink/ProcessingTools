namespace ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;

    using Infrastructure.Net;
    using Models;

    /// <summary>
    /// Implementations of some of the Catalogue Of Life (CoL) API-s.
    /// </summary>
    public class CatalogueOfLifeDataRequester
    {
        /// <summary>
        /// Search scientific name in Catalogue Of Life (CoL).
        /// </summary>
        /// <param name="scientificName">Scientific name of the taxon which rank is searched.</param>
        /// <returns>XmlDocument of the CoL API response.</returns>
        /// <example>http://www.catalogueoflife.org/col/webservice?name=Tara+spinosa&response=full</example>
        public static async Task<XmlDocument> RequestXmlFromCatalogueOfLife(string scientificName)
        {
            string url = $"http://www.catalogueoflife.org/col/webservice?name={scientificName}&response=full";

            try
            {
                return await Connector.GetXmlDocumentAsync(url);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Search scientific name in Catalogue Of Life (CoL).
        /// </summary>
        /// <param name="scientificName">Scientific name of the taxon which rank is searched.</param>
        /// <returns>CatalogueOfLifeApiServiceResponse of the CoL API response.</returns>
        /// <example>http://www.catalogueoflife.org/col/webservice?name=Tara+spinosa&response=full</example>
        public static async Task<CatalogueOfLifeApiServiceResponse> RequestDataFromCatalogueOfLife(string scientificName)
        {
            try
            {
                XmlDocument response = await RequestXmlFromCatalogueOfLife(scientificName);

                CatalogueOfLifeApiServiceResponse result = null;
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(response.OuterXml)))
                {
                    var serializer = new XmlSerializer(typeof(CatalogueOfLifeApiServiceResponse));
                    result = (CatalogueOfLifeApiServiceResponse)serializer.Deserialize(stream);
                }

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}