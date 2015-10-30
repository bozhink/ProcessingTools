namespace ProcessingTools.ServiceClient.ExtractHcmr
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using SystemCommons;

    public class ExtractHcmrResolverDataRequester
    {
        /*
         * https://extract.hcmr.gr/
         * http://tagger.jensenlab.org/GetHTML?document=Both+samples+were+dominated+by+Zetaproteobacteria+Fe+oxidizers.+This+group+was+most+abundant+at+Volcano+1,+where+sediments+were+richer+in+Fe+and+contained+more+crystalline+forms+of+Fe+oxides.&entity_types=-2+-25+-26+-27
         */
        public static async Task<XmlDocument> UseExtractService(string xmlContent)
        {
            XmlDocument result = new XmlDocument();

            if (string.IsNullOrEmpty(xmlContent))
            {
                throw new ArgumentNullException("Content string to send is empty.");
            }

            using (var client = new HttpClient())
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("document", xmlContent);
                values.Add("entity_types", "-25 -26 -27");
                values.Add("format", "xml");

                try
                {
                    using (HttpContent content = new WeakFormUrlEncodedContent(values, Encoding.UTF8))
                    {
                        try
                        {
                            var response = await client.PostAsync("http://tagger.jensenlab.org/GetEntities", content);

                            XmlReader xmlReader = XmlReader.Create(
                                await response.Content.ReadAsStreamAsync(),
                                new XmlReaderSettings
                                {
                                    Async = true,
                                    DtdProcessing = DtdProcessing.Ignore,
                                    IgnoreProcessingInstructions = true,
                                });

                            result.Load(xmlReader);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }

            return result;
        }
    }
}
