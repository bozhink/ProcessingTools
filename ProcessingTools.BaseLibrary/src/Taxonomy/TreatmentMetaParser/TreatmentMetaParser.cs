namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Xml;
    using Configurator;
    using Contracts;

    public abstract class TreatmentMetaParser : TaggerBase, IBaseParser
    {
        protected const string SelectTreatmentGeneraXPathString = "//tp:taxon-treatment[string(tp:treatment-meta/kwd-group/kwd/named-content[@content-type='order'])='ORDO' or string(tp:treatment-meta/kwd-group/kwd/named-content[@content-type='family'])='FAMILIA']/tp:nomenclature/tn/tn-part[@type='genus']";

        protected const string TreatmentMetaReplaceXPathTemplate = "//tp:taxon-treatment[string(tp:nomenclature/tn/tn-part[@type='genus'])='{0}']/tp:treatment-meta/kwd-group/kwd/named-content[@content-type='{1}']";

        private static bool delay = false;

        private ILogger logger;

        public TreatmentMetaParser(string xml, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
        }

        public TreatmentMetaParser(Config config, string xml, ILogger logger)
            : base(config, xml)
        {
            this.logger = logger;
        }

        public TreatmentMetaParser(IBase baseObject, ILogger logger)
            : base(baseObject)
        {
            this.logger = logger;
        }

        public abstract void Parse();

        protected void Delay()
        {
            if (delay)
            {
                Thread.Sleep(500);
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

                this.logger?.Log("{0}: {1}\t--\t{2}", genus, type, taxonName);

                string xpath = string.Format(TreatmentMetaReplaceXPathTemplate, genus, type);
                foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
                {
                    node.InnerXml = taxonName;
                }
            }
            else
            {
                this.logger?.Log("WARNING: Multiple or zero matches of type {0}:", type);
                foreach (string taxonName in higherTaxaOfType)
                {
                    this.logger?.Log("{0}: {1}\t--\t{2}", genus, type, taxonName);
                }

                this.logger?.Log();
            }
        }
    }
}