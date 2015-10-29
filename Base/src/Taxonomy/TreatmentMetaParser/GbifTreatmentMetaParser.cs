namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using Configurator;
    using Contracts;
    using Models.Json.Gbif;

    public class GbifTreatmentMetaParser : TreatmentMetaParser
    {
        private ILogger logger;

        public GbifTreatmentMetaParser(string xml, ILogger logger)
            : base(xml, logger)
        {
            this.logger = logger;
        }

        public GbifTreatmentMetaParser(Config config, string xml, ILogger logger)
            : base(config, xml, logger)
        {
            this.logger = logger;
        }

        public GbifTreatmentMetaParser(IBase baseObject, ILogger logger)
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

                    GbifResult gbifResult = Net.SearchGbif(genus);
                    if ((gbifResult != null) && (gbifResult.CanonicalName != null || gbifResult.ScientificName != null))
                    {
                        this.logger?.Log(
                            "{0} .... {1} .... {2}",
                            genus,
                            gbifResult.ScientificName,
                            gbifResult.CanonicalName);

                        if (!gbifResult.CanonicalName.Equals(genus) && !gbifResult.ScientificName.Contains(genus))
                        {
                            this.logger?.Log("No match.");
                        }
                        else
                        {
                            this.logger?.Log(
                                "Kingdom: {0}\nOrder: {1}\nFamily: {2}\n",
                                gbifResult.Kingdom,
                                gbifResult.Order,
                                gbifResult.Family);

                            List<string> responseKingdom = new List<string>();
                            responseKingdom.Add(gbifResult.Kingdom);

                            List<string> responseOrder = new List<string>();
                            responseOrder.Add(gbifResult.Order);

                            List<string> responseFamily = new List<string>();
                            responseFamily.Add(gbifResult.Family);

                            if (gbifResult.Alternatives != null)
                            {
                                foreach (var alternative in gbifResult.Alternatives)
                                {
                                    if (alternative.CanonicalName.Equals(genus) || alternative.ScientificName.Contains(genus))
                                    {
                                        responseKingdom.Add(alternative.Kingdom);
                                        responseOrder.Add(alternative.Order);
                                        responseFamily.Add(alternative.Family);
                                    }
                                }
                            }

                            this.ReplaceTreatmentMetaClassificationItem(responseKingdom, genus, "kingdom");
                            this.ReplaceTreatmentMetaClassificationItem(responseOrder, genus, "order");
                            this.ReplaceTreatmentMetaClassificationItem(responseFamily, genus, "family");
                        }
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