namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Xml;
    using Configurator;
    using Contracts.Log;
    using ServiceClient.Bio.CatalogueOfLife;

    public class CoLTreatmentMetaParser : TreatmentMetaParser
    {
        private ILogger logger;

        public CoLTreatmentMetaParser(string xml, ILogger logger)
            : base(xml, logger)
        {
            this.logger = logger;
        }

        public CoLTreatmentMetaParser(Config config, string xml, ILogger logger)
            : base(config, xml, logger)
        {
            this.logger = logger;
        }

        public CoLTreatmentMetaParser(IBase baseObject, ILogger logger)
            : base(baseObject, logger)
        {
            this.logger = logger;
        }

        public override void Parse()
        {
            try
            {
                IEnumerable<string> genusList = this.XmlDocument.GetStringListOfUniqueXmlNodes(SelectTreatmentGeneraXPathString, this.NamespaceManager);

                foreach (string genus in genusList)
                {
                    this.Delay();

                    this.logger?.Log("\n{0}\n", genus);

                    XmlDocument response = CatalogueOfLifeDataRequester.SearchCatalogueOfLife(genus).Result;
                    if (response != null)
                    {
                        IEnumerable<string> responseKingdom = response.GetStringListOfUniqueXmlNodes("/results/result[string(name)='" + genus + "']/classification/taxon[string(rank)='Kingdom']/name", this.NamespaceManager);
                        this.ReplaceTreatmentMetaClassificationItem(responseKingdom, genus, "kingdom");

                        IEnumerable<string> responseOrder = response.GetStringListOfUniqueXmlNodes("/results/result[string(name)='" + genus + "']/classification/taxon[string(rank)='Order']/name", this.NamespaceManager);
                        this.ReplaceTreatmentMetaClassificationItem(responseOrder, genus, "order");

                        IEnumerable<string> responseFamily = response.GetStringListOfUniqueXmlNodes("/results/result[string(name)='" + genus + "']/classification/taxon[string(rank)='Family']/name", this.NamespaceManager);
                        this.ReplaceTreatmentMetaClassificationItem(responseFamily, genus, "family");
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}