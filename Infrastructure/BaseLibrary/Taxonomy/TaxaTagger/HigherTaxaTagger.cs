namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Contracts;
    using ProcessingTools.Xml.Extensions;

    public class HigherTaxaTagger : TaxaTagger, IDocumentTagger
    {
        private const string HigherTaxaXPathTemplate = "//p[{0}]|//td[{0}]|//th[{0}]|//li[{0}]|//article-title[{0}]|//title[{0}]|//label[{0}]|//ref[{0}]|//kwd[{0}]|//tp:nomenclature-citation[{0}]|//*[@object_id='95'][{0}]|//value[../@id!='244'][../@id!='434'][../@id!='433'][../@id!='432'][../@id!='431'][../@id!='430'][../@id!='429'][../@id!='428'][../@id!='427'][../@id!='426'][../@id!='425'][../@id!='424'][../@id!='423'][../@id!='422'][../@id!='421'][../@id!='420'][../@id!='419'][../@id!='417'][../@id!='48'][{0}]";

        private readonly IHigherTaxaDataMiner miner;
        private readonly ILogger logger;

        public HigherTaxaTagger(IHigherTaxaDataMiner miner, IBiotaxonomicBlackListIterableDataService service, ILogger logger)
            : base(service)
        {
            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            this.miner = miner;
            this.logger = logger;
        }

        public override async Task<object> Tag(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var textContent = document.XmlDocument.GetTextContent();
            var data = await this.miner.Mine(textContent);

            var plausibleTaxaNames = new HashSet<string>(data.Where(s => s[0] == s.ToUpper()[0]));

            // Blacklist items
            var taxaNames = await this.ClearFakeTaxaNames(document, plausibleTaxaNames);

            var tagModel = this.CreateNewTaxonNameXmlElement(document, TaxonType.Higher);
            await taxaNames.TagContentInDocument(
                tagModel,
                HigherTaxaXPathTemplate,
                document,
                false,
                true,
                this.logger);

            return true;
        }
    }
}
