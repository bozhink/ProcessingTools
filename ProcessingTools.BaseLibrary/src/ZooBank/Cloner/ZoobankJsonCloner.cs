namespace ProcessingTools.BaseLibrary.ZooBank
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using Contracts.Log;
    using Infrastructure.Serialization.Json;
    using ServiceClient.Bio.ZooBank.Models.Json;

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
                if (string.IsNullOrEmpty(value))
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

        private string GetNomenclatureTaxonObjectIdXPath(NomenclaturalAct nomenclaturalAct)
        {
            const string XPathFormat = "//tp:taxon-treatment/tp:nomenclature/tn[string(../tp:taxon-status)='{0}']{1}/object-id[@content-type='zoobank']";
            switch (nomenclaturalAct.RankGroup.ToLower())
            {
                case "family":
                    return string.Format(XPathFormat, "fam. n.", $"[tn-part[@type='family']='{nomenclaturalAct.NameString}']");

                case "genus":
                    return string.Format(XPathFormat, "gen. n.", $"[tn-part[@type='genus']='{nomenclaturalAct.NameString}']");

                case "species":
                    return string.Format(XPathFormat, "sp. n.", $"[tn-part[@type='genus']='{nomenclaturalAct.Parentname}'][tn-part[@type='species']='{nomenclaturalAct.NameString}']");

                default:
                    return null;
            }
        }

        private ZooBankRegistration GetZoobankRegistrationObject()
        {
            List<ZooBankRegistration> zoobankRegistrationList = JsonSerializer.Deserialize<List<ZooBankRegistration>>(this.JsonString);
            ZooBankRegistration zoobankRegistration = null;

            if (zoobankRegistrationList.Count < 1)
            {
                throw new ApplicationException("No valid ZooBank registation records in JSON file");
            }
            else
            {
                if (zoobankRegistrationList.Count > 1)
                {
                    this.logger?.Log(LogType.Warning, "More than one ZooBank registration records in JSON File.\n\tIt will be used only the first one.");
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

                this.logger?.Log(
                    "\n\n{0}{1}{2} {3}",
                    nomenclaturalAct.Parentname,
                    string.IsNullOrEmpty(nomenclaturalAct.Parentname) ? string.Empty : " ",
                    nomenclaturalAct.NameString,
                    nomenclaturalAct.TnuUuid);

                string xpath = this.GetNomenclatureTaxonObjectIdXPath(nomenclaturalAct);
                if (xpath != null)
                {
                    XmlNode objectId = this.XmlDocument.SelectSingleNode(xpath, this.NamespaceManager);
                    if (objectId != null)
                    {
                        objectId.InnerText = ZoobankCloner.ZooBankPrefix + nomenclaturalAct.TnuUuid;
                        numberOfNewNomenclaturalActs++;

                        this.logger?.Log(
                            "{0}{1}{2} {3}",
                            nomenclaturalAct.Parentname,
                            string.IsNullOrEmpty(nomenclaturalAct.Parentname) ? string.Empty : " ",
                            nomenclaturalAct.NameString,
                            nomenclaturalAct.TnuUuid);
                    }
                }
            }

            this.logger?.Log(
                "\n\n\nNumber of nomenclatural acts = {0}.\nNumber of new nomenclatural acts = {1}.\n\n\n",
                numberOfNomenclaturalActs,
                numberOfNewNomenclaturalActs);
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