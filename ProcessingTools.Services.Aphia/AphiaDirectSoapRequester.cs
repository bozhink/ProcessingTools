namespace ProcessingTools.Services.Aphia
{
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Xml;

    public class AphiaDirectSoapRequester
    {
        public static string XmlHttpRequest(string url, string xmlContent)
        {
            string response = null;

            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;

            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(xmlContent);

                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentLength = bytes.Length;
                httpWebRequest.ContentType = "text/xml; encoding='utf-8'";

                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = httpWebResponse.GetResponseStream();
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        response = reader.ReadToEnd();
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                httpWebResponse.Close();
                httpWebResponse = null;
                httpWebRequest = null;
            }

            return response;
        }

        public static XmlDocument XmlHttpRequest(string url, XmlDocument xml)
        {
            XmlDocument response = new XmlDocument();

            response.LoadXml(XmlHttpRequest(url, xml.OuterXml));

            return response;
        }

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

        public static XmlDocument SearchAphia(string scientificName)
        {
            return AphiaDirectSoapRequester.XmlHttpRequest("http://www.marinespecies.org/aphia.php?p=soap", AphiaDirectSoapRequester.AphiaSoapXml(scientificName));
        }
    }
}
