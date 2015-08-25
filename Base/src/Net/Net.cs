namespace ProcessingTools.Base
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    public class Net
    {
        public static string XmlHttpRequest(string urlString, string xmlContent)
        {
            string response = null;

            // Declare an HTTP-specific implementation of the WebRequest class
            HttpWebRequest httpWebRequest = null;

            // Declare an HTTP-specific implementation of the WebResponse class
            HttpWebResponse httpWebResponse = null;

            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(xmlContent);

                // Creates an HttpWebRequest for the specified URL.
                httpWebRequest = (HttpWebRequest)WebRequest.Create(urlString);

                // Set HttpWebRequest properties
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentLength = bytes.Length;
                httpWebRequest.ContentType = "text/xml; encoding='utf-8'";

                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    // Writes a sequence of bytes to the current stream 
                    requestStream.Write(bytes, 0, bytes.Length);
                }

                // Sends the HttpWebRequest, and waits for a response.
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                httpWebResponse.Close();
                httpWebResponse = null;
                httpWebRequest = null;
            }

            return response;
        }

        public static XmlDocument XmlHttpRequest(string urlString, XmlDocument xml)
        {
            XmlDocument response = new XmlDocument();

            // Declare an HTTP-specific implementation of the WebRequest class
            HttpWebRequest httpWebRequest = null;

            // Declare an HTTP-specific implementation of the WebResponse class
            HttpWebResponse httpWebResponse = null;

            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(xml.OuterXml);

                // Creates an HttpWebRequest for the specified URL.
                httpWebRequest = (HttpWebRequest)WebRequest.Create(urlString);

                // Set HttpWebRequest properties
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentLength = bytes.Length;
                httpWebRequest.ContentType = "text/xml; encoding='utf-8'";

                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    // Writes a sequence of bytes to the current stream 
                    requestStream.Write(bytes, 0, bytes.Length);
                }

                // Sends the HttpWebRequest, and waits for a response.
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = httpWebResponse.GetResponseStream();
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        response.InnerXml = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                httpWebResponse.Close();
                httpWebResponse = null;
                httpWebRequest = null;
            }

            return response;
        }

        public static XmlDocument AphiaSoapXml(string scientificName)
        {
            XmlDocument xml = new XmlDocument();
            xml.InnerXml = @"<?xml version=""1.0""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
    xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
    xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Body>
        <getAphiaRecords xmlns=""http://tempuri.org/"">
            <scientificname>" + scientificName + @"</scientificname>
            <marine_only>flase</marine_only>
        </getAphiaRecords>
    </soap:Body>
</soap:Envelope>";
            return xml;
        }

        public static XmlDocument SearchAphia(string scientificName)
        {
            XmlDocument response = Net.XmlHttpRequest("http://www.marinespecies.org/aphia.php?p=soap", Net.AphiaSoapXml(scientificName));
            return response;
        }

        public static Json.Gbif.GbifResult SearchGbif(string scientificName)
        {
            Json.Gbif.GbifResult obj = null;
            try
            {
                using (var client = new HttpClient())
                {
                    string responseString = client.GetStringAsync("http://api.gbif.org/v0.9/species/match?verbose=true&name=" + scientificName).Result;
                    DataContractJsonSerializer data = new DataContractJsonSerializer(typeof(Json.Gbif.GbifResult));
                    MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(responseString));
                    obj = (Json.Gbif.GbifResult)data.ReadObject(stream);
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0);
            }

            return obj;
        }

        /// <summary>
        /// Search scientific name in Catalogue Of Life (CoL)
        /// </summary>
        /// <param name="scientificName">scientific name of the taxon which rank is searched</param>
        /// <returns>taxonomic rank of the scientific name</returns>
        public static XmlDocument SearchCatalogueOfLife(string scientificName)
        {
            // http://www.catalogueoflife.org/col/webservice?name=Tara+spinosa&response=full
            XmlDocument xml = null;
            try
            {
                using (var client = new HttpClient())
                {
                    string responseString = client.GetStringAsync("http://www.catalogueoflife.org/col/webservice?name=" + scientificName + "&response=full").Result;
                    xml = new XmlDocument();
                    xml.LoadXml(responseString);
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0);
            }

            return xml;
        }

        /// <summary>
        /// Search scientific name in The Paleobiology Database (PBDB)
        /// </summary>
        /// <param name="scientificName">scientific name of the taxon which rank is searched</param>
        /// <returns>taxonomic rank of the scientific name</returns>
        public static string SearchNameInPaleobiologyDatabase(string scientificName)
        {
            /*
             * https://paleobiodb.org/data1.1/taxa/single.txt?name=Dascillidae
             * "taxon_no","orig_no","record_type","associated_records","rank","taxon_name","common_name","status","parent_no","senior_no","reference_no","is_extant"
             * "69296","69296","taxon","","family","Dascillidae","soft bodied plant beetle","belongs to","69295","69296","5056","1"
             */
            string result = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    string responseString = client.GetStringAsync("https://paleobiodb.org/data1.1/taxa/single.txt?name=" + scientificName).Result;
                    Alert.Log(responseString);

                    string keys = Regex.Match(responseString, "\\A[^\r\n]+").Value;
                    string values = Regex.Match(responseString, "\n[^\r\n]+").Value;
                    Match matchKeys = Regex.Match(keys, "(?<=\")[^,\"]*(?=\")");
                    Match matchValues = Regex.Match(values, "(?<=\")[^,\"]*(?=\")");
                    Hashtable response = new Hashtable();

                    while (matchKeys.Success && matchValues.Success)
                    {
                        response.Add(matchKeys.Value, matchValues.Value);
                        matchKeys = matchKeys.NextMatch();
                        matchValues = matchValues.NextMatch();
                    }

                    ICollection responseKeys = response.Keys;
                    foreach (var str in responseKeys)
                    {
                        Alert.Log(str + " --- " + response[str]);
                    }

                    if (response["taxon_name"].ToString().CompareTo(scientificName) == 0)
                    {
                        result = response["rank"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0);
            }

            return result;
        }

        public static Json.Pbdb.PbdbAllParents SearchParentsInPaleobiologyDatabase(string scientificName)
        {
            //// https://paleobiodb.org/data1.1/taxa/list.json?name=Dascillidae&rel=all_parents

            Json.Pbdb.PbdbAllParents obj = null;
            try
            {
                using (var client = new HttpClient())
                {
                    string responseString = client.GetStringAsync("https://paleobiodb.org/data1.1/taxa/list.json?name=" + scientificName + "&rel=all_parents").Result;
                    DataContractJsonSerializer data = new DataContractJsonSerializer(typeof(Json.Pbdb.PbdbAllParents));
                    MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(responseString));
                    obj = (Json.Pbdb.PbdbAllParents)data.ReadObject(stream);
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0);
            }

            return obj;
        }

        public static XmlDocument SearchWithGlobalNamesResolver(string[] scientificNames, int[] sourceId = null)
        {
            XmlDocument xmlResult = new XmlDocument();
            try
            {
                string searchString;
                {
                    StringBuilder searchStringBuilder = new StringBuilder();
                    searchStringBuilder.Clear();
                    for (int i = 0, len1 = scientificNames.Length - 1; i <= len1; i++)
                    {
                        searchStringBuilder.Append(Regex.Replace(scientificNames[i].Trim(), "\\s+", "+"));
                        if (i < len1)
                        {
                            searchStringBuilder.Append("|");
                        }
                    }

                    if (sourceId != null)
                    {
                        searchStringBuilder.Append("&data_source_ids=");
                        for (int i = 0, len1 = sourceId.Length - 1; i <= len1; i++)
                        {
                            searchStringBuilder.Append(sourceId[i]);
                            if (i < len1)
                            {
                                searchStringBuilder.Append("|");
                            }
                        }
                    }

                    Alert.Log(searchStringBuilder.ToString());
                    searchString = searchStringBuilder.ToString();
                }

                using (var client = new HttpClient())
                {
                    string responseString = client.GetStringAsync("http://resolver.globalnames.org/name_resolvers.xml?names=" + searchString).Result;
                    ////responseString = Regex.Replace(responseString, "(?<=<(classification-path|classification-path-ranks|classification-path-ids)>)\\||\\|(?=</(classification-path|classification-path-ranks|classification-path-ids)>)", "");
                    xmlResult.LoadXml(responseString);
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0);
            }

            return xmlResult;
        }

        /*
         * https://extract.hcmr.gr/
         * http://tagger.jensenlab.org/GetHTML?document=Both+samples+were+dominated+by+Zetaproteobacteria+Fe+oxidizers.+This+group+was+most+abundant+at+Volcano+1,+where+sediments+were+richer+in+Fe+and+contained+more+crystalline+forms+of+Fe+oxides.&entity_types=-2+-25+-26+-27
         */
        public static XmlDocument UseGreekTagger(string xmlContent)
        {
            XmlDocument result = new XmlDocument();

            if (xmlContent != null && xmlContent.Length > 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    Dictionary<string, string> values = new Dictionary<string, string>();
                    values.Add("document", xmlContent);
                    //// values.Add("entity_types", "-2 -25 -26 -27");
                    values.Add("entity_types", "-25 -26 -27");
                    //// values.Add("entity_types", "-2 -22 -23 -24 -25 -26 -27");
                    values.Add("format", "xml");

                    try
                    {
                        using (HttpContent content = new FormUrlEncodedContent(values))
                        {
                            try
                            {
                                Task<HttpResponseMessage> response = client.PostAsync("http://tagger.jensenlab.org/GetEntities", content);
                                Task<string> responseString = response.Result.Content.ReadAsStringAsync();
                                result.LoadXml(responseString.Result);
                            }
                            catch (Exception e)
                            {
                                Alert.RaiseExceptionForMethod(e, 0, 1);
                            }
                        }
                    }
                    catch (Exception contentException)
                    {
                        Alert.RaiseExceptionForMethod(contentException, "UseGreekTagger", 1, 1);
                    }
                }
            }
            else
            {
                Alert.RaiseExceptionForMethod(new ArgumentNullException("Content string to send is empty."), 0, 1);
            }

            return result;
        }
    }
}
