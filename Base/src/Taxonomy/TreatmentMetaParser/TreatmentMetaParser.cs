namespace ProcessingTools.Base.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Xml;

    public abstract class TreatmentMetaParser : TaggerBase, IBaseParser
    {
        protected const string SelectTreatmentGeneraXPathString = "//tp:taxon-treatment[string(tp:treatment-meta/kwd-group/kwd/named-content[@content-type='order'])='ORDO' or string(tp:treatment-meta/kwd-group/kwd/named-content[@content-type='family'])='FAMILIA']/tp:nomenclature/tn/tn-part[@type='genus']";

        protected const string TreatmentMetaReplaceXPathTemplate = "//tp:taxon-treatment[string(tp:nomenclature/tn/tn-part[@type='genus'])='{0}']/tp:treatment-meta/kwd-group/kwd/named-content[@content-type='{1}']";

        private static bool delay = false;

        public TreatmentMetaParser(string xml)
            : base(xml)
        {
        }

        public TreatmentMetaParser(Config config, string xml)
            : base(config, xml)
        {
        }

        public TreatmentMetaParser(IBase baseObject)
            : base(baseObject)
        {
        }

        public abstract void Parse();

        protected void Delay()
        {
            if (delay)
            {
                Thread.Sleep(15000);
            }
            else
            {
                delay = true;
            }
        }

        protected void ReplaceTreatmentMetaClassificationItem(IEnumerable<string> higherTaxaOfType, string genus, string type)
        {
            if (higherTaxaOfType.Count() == 1)
            {
                string taxonName = higherTaxaOfType.First();

                Alert.Log("{0}: {1}\t--\t{2}", genus, type, taxonName);

                string xpath = string.Format(TreatmentMetaReplaceXPathTemplate, genus, type);
                foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
                {
                    node.InnerXml = taxonName;
                }
            }
            else
            {
                Alert.Log("WARNING: Multiple or zero matches of type {0}:", type);
                foreach (string taxonName in higherTaxaOfType)
                {
                    Alert.Log("{0}: {1}\t--\t{2}", genus, type, taxonName);
                }

                Alert.Log();
            }
        }
    }
}