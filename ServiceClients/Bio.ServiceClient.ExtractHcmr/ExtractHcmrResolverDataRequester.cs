namespace ProcessingTools.Bio.ServiceClient.ExtractHcmr
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using Infrastructure.Net;

    public class ExtractHcmrResolverDataRequester
    {
        /*
         * https://extract.hcmr.gr/
         * http://tagger.jensenlab.org/GetHTML?document=Both+samples+were+dominated+by+Zetaproteobacteria+Fe+oxidizers.+This+group+was+most+abundant+at+Volcano+1,+where+sediments+were+richer+in+Fe+and+contained+more+crystalline+forms+of+Fe+oxides.&entity_types=-2+-25+-26+-27
         */
        public static async Task<XmlDocument> UseExtractService(string xmlContent)
        {
            const string ApiUrl = "http://tagger.jensenlab.org/GetEntities";

            if (string.IsNullOrEmpty(xmlContent))
            {
                throw new ArgumentNullException("Content string to send is empty.");
            }

            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("document", xmlContent);
            values.Add("entity_types", "-25 -26 -27");
            values.Add("format", "xml");

            try
            {
                return await Connector.PostUrlEncodedXmlAsync(ApiUrl, values, Encoding.UTF8);
            }
            catch
            {
                throw;
            }
        }
    }
}
