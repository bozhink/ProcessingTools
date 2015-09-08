namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class CoLTreatmentMetaParser : TreatmentMetaParser
    {
        public CoLTreatmentMetaParser(string xml)
            : base(xml)
        {
        }

        public CoLTreatmentMetaParser(Config config, string xml)
            : base(config, xml)
        {
        }

        public CoLTreatmentMetaParser(IBase baseObject)
            : base(baseObject)
        {
        }

        public override void Parse()
        {
            try
            {
                IEnumerable<string> genusList = this.XmlDocument.GetStringListOfUniqueXmlNodes(SelectTreatmentGeneraXPathString, this.NamespaceManager);

                foreach (string genus in genusList)
                {
                    this.Delay();

                    Alert.Log("\n{0}\n", genus);

                    XmlDocument response = Net.SearchCatalogueOfLife(genus);
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
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }
        }
    }
}