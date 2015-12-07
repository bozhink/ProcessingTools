namespace ProcessingTools.Bio.Taxonomy.ServiceClient.Aphia
{
    using System.Threading.Tasks;
    using System.Xml;
    using Infrastructure.Net;

    public class AphiaDirectSoapRequester
    {
        public static XmlDocument AphiaSoapXml(string scientificName)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(@"<?xml version=""1.0""?>
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

        public static async Task<XmlDocument> SearchAphia(string scientificName)
        {
            const string ApiUrl = "http://www.marinespecies.org/aphia.php?p=soap";

            return await Connector.PostToXmlAsync(
                ApiUrl,
                AphiaDirectSoapRequester.AphiaSoapXml(scientificName).OuterXml,
                "text/xml; encoding='utf-8'");
        }
    }
}
