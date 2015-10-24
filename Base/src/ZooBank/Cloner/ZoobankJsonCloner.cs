namespace ProcessingTools.BaseLibrary.ZooBank
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using Globals.Loggers;
    using Models.Json;
    using Models.Json.ZooBank;

    public class ZoobankJsonCloner : ZoobankCloner
    {
        private string jsonString;

        private ILogger logger;

        public ZoobankJsonCloner(string xmlContent, ILogger logger)
            : base(xmlContent)
        {
            this.logger = logger;
            this.JsonString = null;
        }

        public ZoobankJsonCloner(string jsonString, string xmlContent, ILogger logger)
            : base(xmlContent)
        {
            this.logger = logger;
            this.JsonString = jsonString;
        }

        public string JsonString
        {
            get
            {
                return this.jsonString;
            }

            set
            {
                if (value == null || value.Length < 1)
                {
                    throw new ArgumentNullException("JSON string should not be null or empty.");
                }

                this.jsonString = value;
            }
        }

        public override void Clone()
        {
            try
            {
                ZooBankRegistration zoobankRegistration = this.GetZoobankRegistrationObject();

                this.ProcessArticleLsid(zoobankRegistration);

                this.ProcessTaxonomicActsLsid(zoobankRegistration);
            }
            catch
            {
                throw;
            }
        }

        private string GetNomenclatureTaxonXPath(NomenclaturalAct nomenclaturalAct)
        {
            string xpath = "//tp:taxon-treatment/tp:nomenclature/tn";
            switch (nomenclaturalAct.RankGroup)
            {
                case "Genus":
                    xpath += "[tn-part[@type='genus']='" + nomenclaturalAct.NameString + "'][string(../tp:taxon-status)='gen. n.']/object-id[@content-type='zoobank']";
                    break;

                case "Species":
                    xpath += "[tn-part[@type='genus']='" + nomenclaturalAct.Parentname + "'][tn-part[@type='species']='" + nomenclaturalAct.NameString + "'][string(../tp:taxon-status)='sp. n.']/object-id[@content-type='zoobank']";
                    break;
            }

            return xpath;
        }

        private ZooBankRegistration GetZoobankRegistrationObject()
        {
            List<ZooBankRegistration> zoobankRegistrationList = JsonSerializer.Serialize<List<ZooBankRegistration>>(this.JsonString);
            ZooBankRegistration zoobankRegistration = null;

            if (zoobankRegistrationList.Count < 1)
            {
                throw new ApplicationException("No valid ZooBank registation records in JSON File");
            }
            else
            {
                if (zoobankRegistrationList.Count > 1)
                {
                    this.logger?.Log("WARNING: More than one ZooBank registration records in JSON File.\n\tIt will be used only the first one.");
                }

                zoobankRegistration = zoobankRegistrationList[0];
            }

            return zoobankRegistration;
        }

        private void ProcessArticleLsid(ZooBankRegistration zoobankRegistration)
        {
            string articleLsid = ZooBankPrefix + zoobankRegistration.ReferenceUuid;
            XmlNode selfUri = this.XmlDocument.SelectSingleNode(ArticleZooBankSelfUriXPath, this.NamespaceManager);
            if (selfUri == null)
            {
                throw new ApplicationException("article-meta/self-uri/@content-type='zoobank' is missing.");
            }

            selfUri.InnerText = articleLsid;
        }

        private void ProcessTaxonomicActsLsid(ZooBankRegistration zoobankRegistration)
        {
            int numberOfNomenclaturalActs = zoobankRegistration.NomenclaturalActs.Count;
            int numberOfNewNomenclaturalActs = 0;

            foreach (NomenclaturalAct nomenclaturalAct in zoobankRegistration.NomenclaturalActs)
            {
                this.ResolveEmptyParentNames(zoobankRegistration, nomenclaturalAct);

                this.logger?.Log("\n\n" + nomenclaturalAct.Parentname + (nomenclaturalAct.Parentname == string.Empty ? string.Empty : " ") + nomenclaturalAct.NameString + " " + nomenclaturalAct.TnuUuid);

                string xpath = this.GetNomenclatureTaxonXPath(nomenclaturalAct);

                XmlNode objectId = this.XmlDocument.SelectSingleNode(xpath, this.NamespaceManager);
                if (objectId != null)
                {
                    objectId.InnerText = ZoobankCloner.ZooBankPrefix + nomenclaturalAct.TnuUuid;
                    numberOfNewNomenclaturalActs++;

                    this.logger?.Log(nomenclaturalAct.Parentname + (nomenclaturalAct.Parentname == string.Empty ? string.Empty : " ") + nomenclaturalAct.NameString + " " + nomenclaturalAct.TnuUuid);
                }
            }

            this.logger?.Log("\n\n\nNumber of nomenclatural acts = {0}.\nNumber of new nomenclatural acts = {1}.\n\n\n", numberOfNomenclaturalActs, numberOfNewNomenclaturalActs);
        }

        private void ResolveEmptyParentNames(ZooBankRegistration zoobankRegistration, NomenclaturalAct nomenclaturalAct)
        {
            if (nomenclaturalAct.Parentname == string.Empty && nomenclaturalAct.ParentUsageUuid != string.Empty)
            {
                foreach (NomenclaturalAct n in zoobankRegistration.NomenclaturalActs)
                {
                    if (string.Compare(nomenclaturalAct.ParentUsageUuid, n.TnuUuid) == 0)
                    {
                        nomenclaturalAct.Parentname = n.NameString;
                        break;
                    }
                }
            }
        }
    }
}