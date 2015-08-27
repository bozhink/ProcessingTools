namespace ProcessingTools.Base.ZooBank
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Json.ZooBank;

    public class ZoobankCloner : TaggerBase
    {
        private const string ArticleZooBankSelfUriXPath = "/article/front/article-meta/self-uri[@content-type='zoobank']";
        private const string ContributorZooBankUriXPath = "/article/front/article-meta/contrib-group/contrib/uri[@content-type='zoobank']";
        private const string NomenclatureXPath = "//tp:taxon-treatment/tp:nomenclature";
        private const string ZooBankObjectIdXPath = ".//object-id[@content-type='zoobank']";
        private const string ZooBankPrefix = "http://zoobank.org/";

        private XmlDocument nlmDocument;
        private string nlmXml;

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

            this.CloneTaxonomicActsLsid();
            this.CloneArticleLsid();
            this.CloneAuthorsLsid();
        }

        public void CloneJsonToXml(string jsonString)
        {
            try
            {
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<ZooBankRegistration>));
                List<ZooBankRegistration> zoobankRegistrationList = (List<ZooBankRegistration>)jsonSerializer.ReadObject(stream);
                ZooBankRegistration zoobankRegistration = null;

                if (zoobankRegistrationList.Count < 1)
                {
                    Alert.Die(1, "ERROR: No valid ZooBank registation records in JSON File");
                }
                else if (zoobankRegistrationList.Count > 1)
                {
                    Alert.Log("WARNING: More than one ZooBank registration records in JSON File");
                    Alert.Log("         It will be used only the first one");

                    zoobankRegistration = zoobankRegistrationList[0];
                }
                else
                {
                    zoobankRegistration = zoobankRegistrationList[0];
                }

                // Article lsid
                {
                    string articleLsid = ZooBankPrefix + zoobankRegistration.referenceuuid;
                    XmlNode selfUri = this.XmlDocument.SelectSingleNode(ArticleZooBankSelfUriXPath, this.NamespaceManager);
                    if (selfUri == null)
                    {
                        Alert.Die(1, "ERROR: article-meta/self-uri/@content-type='zoobank' is missing.\n\n");
                    }

                    selfUri.InnerText = articleLsid;
                }

                // Taxonomic acts’ lsid
                {
                    int numberOfNomenclaturalActs = zoobankRegistration.NomenclaturalActs.Count;
                    int numberOfNewNomenclaturalActs = 0;

                    foreach (NomenclaturalAct nomenclaturalAct in zoobankRegistration.NomenclaturalActs)
                    {
                        // First try to resolve empty parent names
                        if (nomenclaturalAct.parentname == string.Empty && nomenclaturalAct.parentusageuuid != string.Empty)
                        {
                            foreach (NomenclaturalAct n in zoobankRegistration.NomenclaturalActs)
                            {
                                if (string.Compare(nomenclaturalAct.parentusageuuid, n.tnuuuid) == 0)
                                {
                                    nomenclaturalAct.parentname = n.namestring;
                                    break;
                                }
                            }
                        }

                        Alert.Log("\n\n" + nomenclaturalAct.parentname + (nomenclaturalAct.parentname == string.Empty ? string.Empty : " ") + nomenclaturalAct.namestring + " " + nomenclaturalAct.tnuuuid);

                        string xpath = "//tp:taxon-treatment/tp:nomenclature/tp:taxon-name";
                        switch (nomenclaturalAct.rankgroup)
                        {
                            case "Genus":
                                xpath += "[tp:taxon-name-part[@taxon-name-part-type='genus']='" + nomenclaturalAct.namestring + "'][string(../tp:taxon-status)='gen. n.']/object-id[@content-type='zoobank']";
                                break;
                            case "Species":
                                xpath += "[tp:taxon-name-part[@taxon-name-part-type='genus']='" + nomenclaturalAct.parentname + "'][tp:taxon-name-part[@taxon-name-part-type='species']='" + nomenclaturalAct.namestring + "'][string(../tp:taxon-status)='sp. n.']/object-id[@content-type='zoobank']";
                                break;
                        }

                        XmlNode objectId = this.XmlDocument.SelectSingleNode(xpath, this.NamespaceManager);
                        if (objectId != null)
                        {
                            objectId.InnerText = ZooBankPrefix + nomenclaturalAct.tnuuuid;
                            numberOfNewNomenclaturalActs++;

                            Alert.Log(nomenclaturalAct.parentname + (nomenclaturalAct.parentname == string.Empty ? string.Empty : " ") + nomenclaturalAct.namestring + " " + nomenclaturalAct.tnuuuid);
                        }
                    }

                    Alert.Log("\n\n\nNumber of nomenclatural acts = " + numberOfNomenclaturalActs + ".\nNumber of new nomenclatural acts = " + numberOfNewNomenclaturalActs + ".\n\n\n");
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 1);
            }
        }

        private void CloneArticleLsid()
        {
            Alert.Log("Reference:");
            try
            {
                XmlNodeList nlmArticleSelfUriList = this.nlmDocument.SelectNodes(ArticleZooBankSelfUriXPath, this.NamespaceManager);
                int nlmArticleSelfUriListCount = nlmArticleSelfUriList.Count;

                XmlNodeList xmlArticleSelfUriList = this.XmlDocument.SelectNodes(ArticleZooBankSelfUriXPath, this.NamespaceManager);
                int xmlArticleSelfUriListCount = xmlArticleSelfUriList.Count;

                if (nlmArticleSelfUriListCount == xmlArticleSelfUriListCount)
                {
                    for (int i = 0; i < xmlArticleSelfUriListCount; ++i)
                    {
                        xmlArticleSelfUriList[i].InnerXml = nlmArticleSelfUriList[i].InnerXml;
                        Alert.Log(xmlArticleSelfUriList[i].InnerXml);
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

        private void CloneAuthorsLsid()
        {
            Alert.Log("Author(s):");
            try
            {
                XmlNodeList xmlContributorList = this.XmlDocument.SelectNodes(ContributorZooBankUriXPath, this.NamespaceManager);
                int xmlContributorListCount = xmlContributorList.Count;

                XmlNodeList nlmContributorList = this.nlmDocument.SelectNodes(ContributorZooBankUriXPath, this.NamespaceManager);
                int nlmContributorListCount = nlmContributorList.Count;

                if (nlmContributorListCount == xmlContributorListCount)
                {
                    for (int i = 0; i < xmlContributorListCount; ++i)
                    {
                        xmlContributorList[i].InnerXml = nlmContributorList[i].InnerXml;
                        Alert.Log(xmlContributorList[i].InnerXml);
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

        private void CloneTaxonomicActsLsid()
        {
            Alert.Log("Taxonomic acts:");
            try
            {
                XmlNodeList nlmNomenclaturesList = this.nlmDocument.SelectNodes(NomenclatureXPath, this.NamespaceManager);
                int nlmNomenclaturesListCount = nlmNomenclaturesList.Count;

                XmlNodeList xmlNomenclaturesList = this.XmlDocument.SelectNodes(NomenclatureXPath, this.NamespaceManager);
                int xmlNomenclaturesListCount = xmlNomenclaturesList.Count;

                if (nlmNomenclaturesListCount == xmlNomenclaturesListCount)
                {
                    for (int i = 0; i < xmlNomenclaturesListCount; ++i)
                    {
                        XmlNodeList xmlObjecIdList = xmlNomenclaturesList[i].SelectNodes(ZooBankObjectIdXPath, this.NamespaceManager);
                        int xmlObjectIdListCount = xmlObjecIdList.Count;

                        if (xmlObjectIdListCount > 0)
                        {
                            XmlNodeList nlmObjecIdList = nlmNomenclaturesList[i].SelectNodes(ZooBankObjectIdXPath, this.NamespaceManager);
                            int nlmObjectIdListCount = nlmObjecIdList.Count;

                            if (xmlObjectIdListCount == nlmObjectIdListCount)
                            {
                                for (int j = 0; j < xmlObjectIdListCount; ++j)
                                {
                                    xmlObjecIdList[j].InnerXml = nlmObjecIdList[j].InnerXml;
                                    Alert.Log(xmlObjecIdList[j].InnerXml);
                                }
                            }
                            else
                            {
                                Alert.Log("Number of ZooBank object-id tags does not match.");
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
    }
}
