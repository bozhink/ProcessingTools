using System;
using System.Xml;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Base.ZooBank
{
    public class ZoobankCloner : Base
    {
        public const string ZooBankPrefix = "http://zoobank.org/";
        private string nlmXml;
        XmlDocument nlmDocument;

        public ZoobankCloner(string _nlmXml)
        {
            this.nlmXml = _nlmXml;
            this.xml = string.Empty;
            this.xmlDocument = new XmlDocument();
            this.nlmDocument = new XmlDocument();
            this.xmlDocument.PreserveWhitespace = true;
            this.nlmDocument.PreserveWhitespace = true;
            try
            {
                this.nlmDocument.LoadXml(nlmXml);
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForType(e, this.GetType().Name, 51);
            }
        }

        public ZoobankCloner(string _nlmXml, string _xmlXml)
        {
            this.nlmXml = _nlmXml;
            this.xml = _xmlXml;
            this.xmlDocument = new XmlDocument();
            this.nlmDocument = new XmlDocument();
            this.xmlDocument.PreserveWhitespace = true;
            this.nlmDocument.PreserveWhitespace = true;
            try
            {
                this.xmlDocument.LoadXml(xml);
                this.nlmDocument.LoadXml(nlmXml);
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForType(e, this.GetType().Name, 51);
            }
        }

        public ZoobankCloner()
        {
            this.xml = string.Empty;
            this.xmlDocument = new XmlDocument();
            this.nlmDocument = new XmlDocument();
            this.xmlDocument.PreserveWhitespace = true;
            this.nlmDocument.PreserveWhitespace = true;
        }

        public void Clone()
        {
            XmlNamespaceManager xmlNamespaceManager = Base.TaxPubNamespceManager(xmlDocument);
            nlmXml = Regex.Replace(nlmXml, @"\s*<!DOCTYPE [^>]*>", "");

            try
            {
                xmlDocument.LoadXml(xml);
                nlmDocument.LoadXml(nlmXml);
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 51);
            }

            XmlNodeList nlmNodeList, nodeList;

            Alert.Message("Taxonomic acts:");
            try
            {
                nlmNodeList = nlmDocument.SelectNodes("//tp:taxon-treatment/tp:nomenclature", xmlNamespaceManager);
                nodeList = xmlDocument.SelectNodes("//tp:taxon-treatment/tp:nomenclature", xmlNamespaceManager);

                if (nlmNodeList.Count == nodeList.Count)
                {
                    for (int i = 0; i < nodeList.Count; i++)
                    {
                        XmlNodeList objecIdList = nodeList[i].SelectNodes(".//object-id[@content-type='zoobank']", xmlNamespaceManager);
                        XmlNodeList nlmObjecIdList = nlmNodeList[i].SelectNodes(".//object-id[@content-type='zoobank']", xmlNamespaceManager);
                        if (objecIdList.Count > 0)
                        {
                            if (objecIdList.Count == nlmObjecIdList.Count)
                            {
                                for (int j = 0; j < objecIdList.Count; j++)
                                {
                                    objecIdList[j].InnerXml = nlmObjecIdList[j].InnerXml;
                                    Alert.Message(objecIdList[j].InnerXml);
                                }
                            }
                            else
                            {
                                Alert.Message("Number of ZooBank objec-id tags does not match.");
                            }
                        }
                    }
                    Alert.Message();
                }
                else
                {
                    Alert.Message("Number of nomenclatures tags in these files does not match.");
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }

            Alert.Message("Reference:");
            try
            {
                nlmNodeList = nlmDocument.SelectNodes("//self-uri", xmlNamespaceManager);
                nodeList = xmlDocument.SelectNodes("//self-uri", xmlNamespaceManager);
                if (nlmNodeList.Count == nodeList.Count)
                {
                    for (int i = 0; i < nodeList.Count; i++)
                    {
                        nodeList[i].InnerXml = nlmNodeList[i].InnerXml;
                        Alert.Message(nodeList[i].InnerXml);
                    }
                    Alert.Message();
                }
                else
                {
                    Alert.Message("Number of ZooBank self-uri tags in these files does not match.");
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }

            Alert.Message("Author(s):");
            try
            {
                nodeList = xmlDocument.SelectNodes("/article/front/article-meta/contrib-group/contrib/uri[@content-type='zoobank']", xmlNamespaceManager);
                nlmNodeList = nlmDocument.SelectNodes("/article/front/article-meta/contrib-group/contrib/uri[@content-type='zoobank']", xmlNamespaceManager);
                if (nlmNodeList.Count == nodeList.Count)
                {
                    for (int i = 0; i < nodeList.Count; i++)
                    {
                        nodeList[i].InnerXml = nlmNodeList[i].InnerXml;
                        Alert.Message(nodeList[i].InnerXml);
                    }
                    Alert.Message();
                }
                else
                {
                    Alert.Message("Number of ZooBank uri tags in these files does not match.");
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }

            xml = xmlDocument.OuterXml;
        }

        public void CloneJsonToXml(string jsonString)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Json.ZooBank.ZooBankRegistration>));
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                List<Json.ZooBank.ZooBankRegistration> zbr = (List<Json.ZooBank.ZooBankRegistration>)ser.ReadObject(stream);
                Json.ZooBank.ZooBankRegistration z = null;

                if (zbr.Count < 1)
                {
                    Alert.Message("ERROR: No valid ZooBank registation records in JSON File");
                    Alert.Exit(1);
                }
                else if (zbr.Count > 1)
                {
                    Alert.Message("WARNING: More than one ZooBank registration records in JSON File");
                    Alert.Message("         It will be used only the first one");
                    z = zbr[0];
                }
                else
                {
                    z = zbr[0];
                }

                ParseXmlStringToXmlDocument();

                // Article lsid
                {
                    string articleLsid = ZooBankPrefix + z.referenceuuid;
                    XmlNode selfUri = xmlDocument.SelectSingleNode("/article/front/article-meta/self-uri[@content-type='zoobank']", namespaceManager);
                    if (selfUri == null)
                    {
                        Alert.Message("ERROR: article-meta/self-uri/@content-type='zoobank' is missing.\n\n");
                        Alert.Exit(1);
                    }
                    selfUri.InnerText = articleLsid;
                }

                // Taxonomic acts’ lsid
                {
                    int numberOfNomenclaturalActs = z.NomenclaturalActs.Count;
                    int numberOfNewNomenclaturalActs = 0;
                    foreach (Json.ZooBank.NomenclaturalAct na in z.NomenclaturalActs)
                    {
                        // First try to resolve empty parent names
                        if (na.parentname == string.Empty && na.parentusageuuid != string.Empty)
                        {
                            foreach (Json.ZooBank.NomenclaturalAct n in z.NomenclaturalActs)
                            {
                                if (string.Compare(na.parentusageuuid, n.tnuuuid) == 0)
                                {
                                    na.parentname = n.namestring;
                                    break;
                                }
                            }
                        }

                        Alert.Message("\n\n");
                        Alert.Message(na.parentname + (na.parentname == string.Empty ? "" : " ") + na.namestring + " " + na.tnuuuid);

                        string xpath = "//tp:taxon-treatment/tp:nomenclature/tp:taxon-name";
                        switch (na.rankgroup)
                        {
                            case "Genus":
                                xpath += "[tp:taxon-name-part[@taxon-name-part-type='genus']='" + na.namestring + "'][string(../tp:taxon-status)='gen. n.']/object-id[@content-type='zoobank']";
                                break;
                            case "Species":
                                xpath += "[tp:taxon-name-part[@taxon-name-part-type='genus']='" + na.parentname + "'][tp:taxon-name-part[@taxon-name-part-type='species']='" + na.namestring + "'][string(../tp:taxon-status)='sp. n.']/object-id[@content-type='zoobank']";
                                break;
                        }
                        XmlNode objectId = xmlDocument.SelectSingleNode(xpath, namespaceManager);
                        if (objectId != null)
                        {
                            objectId.InnerText = ZooBankPrefix + na.tnuuuid;
                            numberOfNewNomenclaturalActs++;

                            Alert.Message(na.parentname + (na.parentname == string.Empty ? "" : " ") + na.namestring + " " + na.tnuuuid);
                        }
                    }

                    Alert.Message("\n\n\nNumber of nomenclatural acts = " + numberOfNomenclaturalActs + ".\nNumber of new nomenclatural acts = " + numberOfNewNomenclaturalActs + ".\n\n\n");
                }

                xml = xmlDocument.OuterXml;
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 1);
            }
        }
    }
}
