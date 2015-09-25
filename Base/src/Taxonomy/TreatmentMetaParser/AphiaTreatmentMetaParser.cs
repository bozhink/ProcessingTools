namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class AphiaTreatmentMetaParser : TreatmentMetaParser
    {
        private ILogger logger;

        public AphiaTreatmentMetaParser(string xml, ILogger logger)
            : base(xml, logger)
        {
            this.logger = logger;
        }

        public AphiaTreatmentMetaParser(Config config, string xml, ILogger logger)
            : base(config, xml, logger)
        {
            this.logger = logger;
        }

        public AphiaTreatmentMetaParser(IBase baseObject, ILogger logger)
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

                    XmlDocument response = Net.SearchAphia(genus);

                    IEnumerable<string> responseKingdom = response.GetStringListOfUniqueXmlNodes("//return/item[string(genus)='" + genus + "']/kingdom", this.NamespaceManager);
                    this.ReplaceTreatmentMetaClassificationItem(responseKingdom, genus, "kingdom");

                    IEnumerable<string> responseOrder = response.GetStringListOfUniqueXmlNodes("//return/item[string(genus)='" + genus + "']/order", this.NamespaceManager);
                    this.ReplaceTreatmentMetaClassificationItem(responseOrder, genus, "order");

                    IEnumerable<string> responseFamily = response.GetStringListOfUniqueXmlNodes("//return/item[string(genus)='" + genus + "']/family", this.NamespaceManager);
                    this.ReplaceTreatmentMetaClassificationItem(responseFamily, genus, "family");
                }
            }
            catch (Exception e)
            {
                this.logger?.LogException(e, string.Empty);
            }
        }
    }
}
