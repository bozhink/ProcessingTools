namespace ProcessingTools.Bio.Taxonomy.ServiceClient.Aphia
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Common;
    using ProcessingTools.Constants;
    using ProcessingTools.Extensions;
    using ProcessingTools.Net.Factories.Contracts;

    public class AphiaDirectSoapRequester
    {
        private const string BaseAddress = "http://www.marinespecies.org";
        private const string ApiUrl = "aphia.php?p=soap";

        private readonly INetConnectorFactory connectorFactory;

        public AphiaDirectSoapRequester(INetConnectorFactory connectorFactory)
        {
            if (connectorFactory == null)
            {
                throw new ArgumentNullException(nameof(connectorFactory));
            }

            this.connectorFactory = connectorFactory;
        }

        public XmlDocument AphiaSoapXml(string scientificName)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(@"<?xml version=""1.0""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
    xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
    xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Body>
        <getAphiaRecords xmlns=""http://tempuri.org/"">
            <scientificname>" + scientificName + @"</scientificname>
            <marine_only>flase</marine_only>
        </getAphiaRecords>
    </soap:Body>
</soap:Envelope>");
            return xml;
        }

        public async Task<XmlDocument> SearchAphia(string scientificName)
        {
            var connector = this.connectorFactory.Create(BaseAddress);
            var response = await connector.Post(
                ApiUrl,
                this.AphiaSoapXml(scientificName).OuterXml,
                ContentTypes.Xml,
                Defaults.DefaultEncoding);

            return response.ToXmlDocument();
        }
    }
}
