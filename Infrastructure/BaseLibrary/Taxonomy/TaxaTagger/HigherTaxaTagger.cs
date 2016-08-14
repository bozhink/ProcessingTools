namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Xml.Extensions;

    public class HigherTaxaTagger : TaxaTagger
    {
        private const string HigherTaxaXPathTemplate = "//p[{0}]|//td[{0}]|//th[{0}]|//li[{0}]|//article-title[{0}]|//title[{0}]|//label[{0}]|//ref[{0}]|//kwd[{0}]|//tp:nomenclature-citation[{0}]|//*[@object_id='95'][{0}]|//value[../@id!='244'][../@id!='434'][../@id!='433'][../@id!='432'][../@id!='431'][../@id!='430'][../@id!='429'][../@id!='428'][../@id!='427'][../@id!='426'][../@id!='425'][../@id!='424'][../@id!='423'][../@id!='422'][../@id!='421'][../@id!='420'][../@id!='419'][../@id!='417'][../@id!='48'][{0}]";

        private ILogger logger;
        private IHigherTaxaDataMiner miner;

        public HigherTaxaTagger(string xml, IHigherTaxaDataMiner miner, IBiotaxonomicBlackListIterableDataService service, ILogger logger)
            : base(xml, service)
        {
            this.logger = logger;
            this.miner = miner;
        }

        public override async Task Tag()
        {
            var textContent = this.XmlDocument.GetTextContent();
            var data = this.miner.Mine(textContent).Result;

            IEnumerable<string> taxaNames = new HashSet<string>(data.Where(s => s[0] == s.ToUpper()[0]));

            // Blacklist items
            taxaNames = await this.ClearFakeTaxaNames(taxaNames);

            XmlElement higherTaxaTag = this.XmlDocument.CreateElement("tn");
            higherTaxaTag.SetAttribute("type", "higher");

            // TODO: Optimize performance.
            taxaNames.TagContentInDocument(
                higherTaxaTag,
                HigherTaxaXPathTemplate,
                this.NamespaceManager,
                this.XmlDocument,
                false,
                true,
                this.logger)
                .Wait();
        }
    }
}