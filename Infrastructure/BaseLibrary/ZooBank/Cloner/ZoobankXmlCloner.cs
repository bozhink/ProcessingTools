namespace ProcessingTools.BaseLibrary.ZooBank
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;

    public class ZoobankXmlCloner : ZoobankCloner
    {
        private XmlDocument nlmDocument;
        private ILogger logger;

        public ZoobankXmlCloner(string xmlContent, ILogger logger)
            : base(xmlContent)
        {
            this.logger = logger;
            this.Init();
        }

        public ZoobankXmlCloner(string nlmXmlContent, string xmlContent, ILogger logger)
            : base(xmlContent)
        {
            this.logger = logger;
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
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("NLM XmlDocument string content should not be null or empty.");
                }

                try
                {
                    this.NlmXmlDocument.LoadXml(value);
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
                    throw new ArgumentNullException("NLM XmlDocument should not be null");
                }

                this.nlmDocument = value;
            }
        }

        public override Task Clone()
        {
            return Task.Run(() =>
            {
                this.CloneTaxonomicActsLsid();
                this.CloneArticleLsid();
                this.CloneAuthorsLsid();
            });
        }

        private void CloneArticleLsid()
        {
            this.logger?.Log("Reference:");
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
                        this.logger?.Log(xmlArticleSelfUriList[i].InnerXml);
                    }

                    this.logger?.Log();
                }
                else
                {
                    this.logger?.Log(LogType.Warning, "Number of ZooBank self-uri tags in these files does not match.");
                }
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }
        }

        private void CloneAuthorsLsid()
        {
            this.logger?.Log("Author(s):");
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
                        this.logger?.Log(xmlContributorList[i].InnerXml);
                    }

                    this.logger?.Log();
                }
                else
                {
                    this.logger?.Log(LogType.Warning, "Number of ZooBank uri tags in these files does not match.");
                }
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }
        }

        private void CloneTaxonomicActsLsid()
        {
            this.logger?.Log("Taxonomic acts:");
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
                                    this.logger?.Log(xmlObjecIdList[j].InnerXml);
                                }
                            }
                            else
                            {
                                this.logger?.Log(LogType.Warning, "Number of ZooBank object-id tags does not match.");
                            }
                        }
                    }

                    this.logger?.Log();
                }
                else
                {
                    this.logger?.Log(LogType.Warning, "Number of nomenclatures tags in these files does not match.");
                }
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
            }
        }

        private void Init()
        {
            this.nlmDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };
        }
    }
}