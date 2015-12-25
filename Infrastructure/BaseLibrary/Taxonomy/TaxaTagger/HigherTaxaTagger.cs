namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;

    using Bio.Taxonomy.Harvesters.Contracts;
    using Bio.Taxonomy.Services.Data.Contracts;
    using Configurator;
    using Contracts;
    using ProcessingTools.Contracts.Log;

    public class HigherTaxaTagger : TaxaTagger
    {
        private const string HigherTaxaXPathTemplate = "//p[{0}]|//td[{0}]|//th[{0}]|//li[{0}]|//article-title[{0}]|//title[{0}]|//label[{0}]|//ref[{0}]|//kwd[{0}]|//tp:nomenclature-citation[{0}]|//*[@object_id='95'][{0}]|//value[../@id!='244'][../@id!='434'][../@id!='433'][../@id!='432'][../@id!='431'][../@id!='430'][../@id!='429'][../@id!='428'][../@id!='427'][../@id!='426'][../@id!='425'][../@id!='424'][../@id!='423'][../@id!='422'][../@id!='421'][../@id!='420'][../@id!='419'][../@id!='417'][../@id!='48'][{0}]";

        private readonly TagContent higherTaxaTag = new TagContent("tn", @" type=""higher""");

        private ILogger logger;
        private IHigherTaxaHarvester harvester;

        public HigherTaxaTagger(Config config, string xml, IHigherTaxaHarvester harvester, IRepositoryDataService<string> blackList, ILogger logger)
            : base(config, xml, blackList)
        {
            this.logger = logger;
            this.harvester = harvester;
        }

        public HigherTaxaTagger(IBase baseObject, IHigherTaxaHarvester harvester, IRepositoryDataService<string> blackList, ILogger logger)
            : base(baseObject, blackList)
        {
            this.logger = logger;
            this.harvester = harvester;
        }

        public override void Tag()
        {
            try
            {
                this.harvester.Harvest(this.TextContent);
                IEnumerable<string> taxaNames = new HashSet<string>(this.harvester.Data.Where(s => s[0] == s.ToUpper()[0]));

                // Blacklist items
                taxaNames = this.ClearFakeTaxaNames(taxaNames);

                // TODO: Optimize peformance.
                taxaNames.TagContentInDocument(this.higherTaxaTag, HigherTaxaXPathTemplate, this.XmlDocument, false, true, this.logger);
            }
            catch
            {
                throw;
            }
        }
    }
}