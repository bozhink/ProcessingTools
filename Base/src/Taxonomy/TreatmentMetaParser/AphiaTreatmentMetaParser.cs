namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using Configurator;
    using Contracts;
    using Services.Aphia;

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
                var aphiaService = new AphiaNameService();

                IEnumerable<string> genusList = this.XmlDocument.GetStringListOfUniqueXmlNodes(SelectTreatmentGeneraXPathString, this.NamespaceManager);

                foreach (string genus in genusList)
                {
                    this.Delay();

                    this.logger?.Log("\n{0}\n", genus);

                    var aphiaRecords = aphiaService.getAphiaRecords(genus, false, true, false, 0);

                    foreach (var record in aphiaRecords)
                    {
                        this.logger?.Log(record?.scientificname);
                    }

                    if (aphiaRecords != null)
                    {
                        var matchedRecords = new HashSet<AphiaRecord>(aphiaRecords.Where(x => string.Compare(x.scientificname, genus, true) == 0));

                        if (matchedRecords != null)
                        {
                            IEnumerable<string> responseKingdom = matchedRecords.Select(x => x.kingdom);
                            this.ReplaceTreatmentMetaClassificationItem(responseKingdom, genus, "kingdom");

                            IEnumerable<string> responseOrder = matchedRecords.Select(x => x.order);
                            this.ReplaceTreatmentMetaClassificationItem(responseOrder, genus, "order");

                            IEnumerable<string> responseFamily = matchedRecords.Select(x => x.family);
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
