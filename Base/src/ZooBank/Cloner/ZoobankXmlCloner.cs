namespace ProcessingTools.BaseLibrary.ZooBank
{
    using System;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class ZoobankXmlCloner : ZoobankCloner
    {
        private XmlDocument nlmDocument;

        public ZoobankXmlCloner(string xmlContent)
            : base(xmlContent)
        {
            this.Init();
        }

        public ZoobankXmlCloner(string nlmXmlContent, string xmlContent)
            : base(xmlContent)
        {
            this.Init();
            this.NlmXml = nlmXmlContent;
        }

        public string NlmXml
        {
            get
            {
                return this.NlmXmlDocument.OuterXml;
            }

            set
            {
                if (value == null || value.Length < 1)
                {
                    throw new ArgumentNullException("NLM XmlDocument string content should not be null or empty.");
                }

                string xmlContent = Regex.Replace(value, @"\s*<!DOCTYPE [^>]*>", string.Empty);
                try
                {
                    this.NlmXmlDocument.LoadXml(xmlContent);
                }
                catch
                {
                    throw;
                }
            }
        }

        public XmlDocument NlmXmlDocument
        {
            get
            {
                return this.nlmDocument;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("NLM XmlDocument schould not be null");
                }

                this.nlmDocument = value;
            }
        }

        public override void Clone()
        {
            this.CloneTaxonomicActsLsid();
            this.CloneArticleLsid();
            this.CloneAuthorsLsid();
        }

        private void CloneArticleLsid()
        {
            Alert.Log("Reference:");
            try
            {
                XmlNodeList nlmArticleSelfUriList = this.NlmXmlDocument.SelectNodes(ArticleZooBankSelfUriXPath, this.NamespaceManager);
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

                XmlNodeList nlmContributorList = this.NlmXmlDocument.SelectNodes(ContributorZooBankUriXPath, this.NamespaceManager);
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
                XmlNodeList nlmNomenclaturesList = this.NlmXmlDocument.SelectNodes(NomenclatureXPath, this.NamespaceManager);
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

        private void Init()
        {
            this.nlmDocument = new XmlDocument();
            this.nlmDocument.PreserveWhitespace = true;
        }
    }
}