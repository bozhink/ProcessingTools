using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace ProcessingTools.Base.ZooBank
{
    public class ZoobankCloner : TaggerBase
    {
        public const string ZooBankPrefix = "http://zoobank.org/";
        private string nlmXml;
        private XmlDocument nlmDocument;

        public ZoobankCloner(string xmlContent)
            : base(xmlContent)
        {
            this.nlmXml = string.Empty;
            this.nlmDocument = new XmlDocument();
            this.nlmDocument.PreserveWhitespace = true;
        }

        public ZoobankCloner(string nlmXmlContent, string xmlContent)
            : base(xmlContent)
        {
            this.nlmXml = nlmXmlContent;
            this.nlmDocument = new XmlDocument();
            this.nlmDocument.PreserveWhitespace = true;
            try
            {
                this.nlmDocument.LoadXml(this.nlmXml);
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForType(e, this.GetType().Name, 51);
            }
        }

        public void Clone()
        {
            this.nlmXml = Regex.Replace(this.nlmXml, @"\s*<!DOCTYPE [^>]*>", string.Empty);

            try
            {
                this.nlmDocument.LoadXml(this.nlmXml);
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 51);
            }

            this.ParseXmlStringToXmlDocument();

            this.CloneTaxonomicActsLsid();
            this.CloneArticleLsid();
            this.CloneAuthorsLsid();

            this.ParseXmlDocumentToXmlString();
        }

        private void CloneAuthorsLsid()
        {
            Alert.Log("Author(s):");
            try
            {
                XmlNodeList nodeList = this.XmlDocument.SelectNodes("/article/front/article-meta/contrib-group/contrib/uri[@content-type='zoobank']", this.NamespaceManager);
                XmlNodeList nlmNodeList = this.nlmDocument.SelectNodes("/article/front/article-meta/contrib-group/contrib/uri[@content-type='zoobank']", this.NamespaceManager);
                if (nlmNodeList.Count == nodeList.Count)
                {
                    for (int i = 0; i < nodeList.Count; i++)
                    {
                        nodeList[i].InnerXml = nlmNodeList[i].InnerXml;
                        Alert.Log(nodeList[i].InnerXml);
                    }

                    Alert.Log();
                }
                else
                {
                    Alert.Log("Number of ZooBank uri tags in these files does not match.");
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }
        }

        private void CloneArticleLsid()
        {
            Alert.Log("Reference:");
            try
            {
                XmlNodeList nlmNodeList = this.nlmDocument.SelectNodes("//self-uri", this.NamespaceManager);
                XmlNodeList nodeList = this.XmlDocument.SelectNodes("//self-uri", this.NamespaceManager);
                if (nlmNodeList.Count == nodeList.Count)
                {
                    for (int i = 0; i < nodeList.Count; i++)
                    {
                        nodeList[i].InnerXml = nlmNodeList[i].InnerXml;
                        Alert.Log(nodeList[i].InnerXml);
                    }

                    Alert.Log();
                }
                else
                {
                    Alert.Log("Number of ZooBank self-uri tags in these files does not match.");
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }
        }

        private void CloneTaxonomicActsLsid()
        {
            Alert.Log("Taxonomic acts:");
            try
            {
                XmlNodeList nlmNodeList = this.nlmDocument.SelectNodes("//tp:taxon-treatment/tp:nomenclature", this.NamespaceManager);
                XmlNodeList nodeList = this.XmlDocument.SelectNodes("//tp:taxon-treatment/tp:nomenclature", this.NamespaceManager);

                if (nlmNodeList.Count == nodeList.Count)
                {
                    for (int i = 0; i < nodeList.Count; i++)
                    {
                        XmlNodeList objecIdList = nodeList[i].SelectNodes(".//object-id[@content-type='zoobank']", this.NamespaceManager);
                        XmlNodeList nlmObjecIdList = nlmNodeList[i].SelectNodes(".//object-id[@content-type='zoobank']", this.NamespaceManager);
                        if (objecIdList.Count > 0)
                        {
                            if (objecIdList.Count == nlmObjecIdList.Count)
                            {
                                for (int j = 0; j < objecIdList.Count; j++)
                                {
                                    objecIdList[j].InnerXml = nlmObjecIdList[j].InnerXml;
                                    Alert.Log(objecIdList[j].InnerXml);
                                }
                            }
                            else
                            {
                                Alert.Log("Number of ZooBank objec-id tags does not match.");
                            }
                        }
                    }

                    Alert.Log();
                }
                else
                {
                    Alert.Log("Number of nomenclatures tags in these files does not match.");
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }
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
                    Alert.Log("ERROR: No valid ZooBank registation records in JSON File");
                    Alert.Exit(1);
                }
                else if (zbr.Count > 1)
                {
                    Alert.Log("WARNING: More than one ZooBank registration records in JSON File");
                    Alert.Log("         It will be used only the first one");
                    z = zbr[0];
                }
                else
                {
                    z = zbr[0];
                }

                this.ParseXmlStringToXmlDocument();

                // Article lsid
                {
                    string articleLsid = ZooBankPrefix + z.referenceuuid;
                    XmlNode selfUri = this.XmlDocument.SelectSingleNode("/article/front/article-meta/self-uri[@content-type='zoobank']", this.NamespaceManager);
                    if (selfUri == null)
                    {
                        Alert.Log("ERROR: article-meta/self-uri/@content-type='zoobank' is missing.\n\n");
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

                        Alert.Log("\n\n");
                        Alert.Log(na.parentname + (na.parentname == string.Empty ? string.Empty : " ") + na.namestring + " " + na.tnuuuid);

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

                        XmlNode objectId = this.XmlDocument.SelectSingleNode(xpath, this.NamespaceManager);
                        if (objectId != null)
                        {
                            objectId.InnerText = ZooBankPrefix + na.tnuuuid;
                            numberOfNewNomenclaturalActs++;

                            Alert.Log(na.parentname + (na.parentname == string.Empty ? string.Empty : " ") + na.namestring + " " + na.tnuuuid);
                        }
                    }

                    Alert.Log("\n\n\nNumber of nomenclatural acts = " + numberOfNomenclaturalActs + ".\nNumber of new nomenclatural acts = " + numberOfNewNomenclaturalActs + ".\n\n\n");
                }

                this.ParseXmlDocumentToXmlString();
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 1);
            }
        }
    }
}
