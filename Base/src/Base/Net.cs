using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Base
{
    public class Net
    {
        public static string XmlHttpRequest(string urlString, string xmlContent)
        {
            string response = null;
            HttpWebRequest httpWebRequest = null; //Declare an HTTP-specific implementation of the WebRequest class
            HttpWebResponse httpWebResponse = null; //Declare an HTTP-specific implementation of the WebResponse class
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(xmlContent);

                //Creates an HttpWebRequest for the specified URL.
                httpWebRequest = (HttpWebRequest)WebRequest.Create(urlString);

                //Set HttpWebRequest properties
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentLength = bytes.Length;
                httpWebRequest.ContentType = "text/xml; encoding='utf-8'";

                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    //Writes a sequence of bytes to the current stream 
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }

                //Sends the HttpWebRequest, and waits for a response.
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    //Get response stream into StreamReader
                    using (Stream responseStream = httpWebResponse.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            response = reader.ReadToEnd();
                        }
                    }
                }

                httpWebResponse.Close();
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
            HttpWebRequest httpWebRequest = null; //Declare an HTTP-specific implementation of the WebRequest class
            HttpWebResponse httpWebResponse = null; //Declare an HTTP-specific implementation of the WebResponse class
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(xml.OuterXml);

                //Creates an HttpWebRequest for the specified URL.
                httpWebRequest = (HttpWebRequest)WebRequest.Create(urlString);

                //Set HttpWebRequest properties
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentLength = bytes.Length;
                httpWebRequest.ContentType = "text/xml; encoding='utf-8'";

                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    //Writes a sequence of bytes to the current stream 
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }

                //Sends the HttpWebRequest, and waits for a response.
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    //Get response stream into StreamReader
                    using (Stream responseStream = httpWebResponse.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            response.InnerXml = reader.ReadToEnd();
                        }
                    }
                }

                httpWebResponse.Close();
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
                    Alert.Message(responseString);

                    string keys = Regex.Match(responseString, "\\A[^\r\n]+").Value;
                    string values = Regex.Match(responseString, "\n[^\r\n]+").Value;
                    Match mKeys = Regex.Match(keys, "(?<=\")[^,\"]*(?=\")");
                    Match mValues = Regex.Match(values, "(?<=\")[^,\"]*(?=\")");
                    Hashtable response = new Hashtable();

                    while (mKeys.Success && mValues.Success)
                    {
                        response.Add(mKeys.Value, mValues.Value);
                        mKeys = mKeys.NextMatch();
                        mValues = mValues.NextMatch();
                    }

                    ICollection responseKeys = response.Keys;
                    foreach (var str in responseKeys)
                    {
                        Alert.Message(str + " --- " + response[str]);
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
            //https://paleobiodb.org/data1.1/taxa/list.json?name=Dascillidae&rel=all_parents

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
                    StringBuilder sBuilder = new StringBuilder();
                    sBuilder.Clear();
                    for (int i = 0, len1 = scientificNames.Length - 1; i <= len1; i++)
                    {
                        sBuilder.Append(Regex.Replace(scientificNames[i].Trim(), "\\s+", "+"));
                        if (i < len1)
                        {
                            sBuilder.Append("|");
                        }
                    }

                    if (sourceId != null)
                    {
                        sBuilder.Append("&data_source_ids=");
                        for (int i = 0, len1 = sourceId.Length - 1; i <= len1; i++)
                        {
                            sBuilder.Append(sourceId[i]);
                            if (i < len1)
                            {
                                sBuilder.Append("|");
                            }
                        }
                    }

                    Alert.Message(sBuilder.ToString());
                    searchString = sBuilder.ToString();
                }

                using (var client = new HttpClient())
                {
                    string responseString = client.GetStringAsync("http://resolver.globalnames.org/name_resolvers.xml?names=" + searchString).Result;
                    //responseString = Regex.Replace(responseString, "(?<=<(classification-path|classification-path-ranks|classification-path-ids)>)\\||\\|(?=</(classification-path|classification-path-ranks|classification-path-ids)>)", "");
                    xmlResult.LoadXml(responseString);
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0);
            }

            return xmlResult;
        }
    }
}
