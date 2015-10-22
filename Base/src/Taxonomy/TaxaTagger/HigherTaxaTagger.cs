namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using Configurator;
    using Globals;
    using Globals.Loggers;

    public class HigherTaxaTagger : TaxaTagger
    {
        public const string HigherTaxaMatchPattern = "\\b([A-Z](?i)[a-z]*(morphae?|mida|toda|ideae|oida|genea|formes|ales|lifera|ieae|indeae|eriae|idea|aceae|oidea|oidae|inae|ini|ina|anae|ineae|acea|oideae|mycota|mycotina|mycetes|mycetidae|phyta|phytina|opsida|phyceae|idae|phycidae|ptera|poda|phaga|itae|odea|alia|ntia|osauria))\\b";

        private const string HigherTaxaXPathTemplate = "//p[{0}]|//td[{0}]|//th[{0}]|//li[{0}]|//article-title[{0}]|//title[{0}]|//label[{0}]|//ref[{0}]|//kwd[{0}]|//tp:nomenclature-citation[{0}]|//value[../@id!='244'][../@id!='434'][../@id!='433'][../@id!='432'][../@id!='431'][../@id!='430'][../@id!='429'][../@id!='428'][../@id!='427'][../@id!='426'][../@id!='425'][../@id!='424'][../@id!='423'][../@id!='422'][../@id!='421'][../@id!='420'][../@id!='419'][../@id!='417'][../@id!='48'][{0}]";

        private readonly TagContent higherTaxaTag = new TagContent("tn", @" type=""higher""");

        private ILogger logger;

        public HigherTaxaTagger(string xml, IStringDataList whiteList, IStringDataList blackList, ILogger logger)
            : base(xml, whiteList, blackList)
        {
            this.logger = logger;
        }

        public HigherTaxaTagger(Config config, string xml, IStringDataList whiteList, IStringDataList blackList, ILogger logger)
            : base(config, xml, whiteList, blackList)
        {
            this.logger = logger;
        }

        public HigherTaxaTagger(IBase baseObject, IStringDataList whiteList, IStringDataList blackList)
            : base(baseObject, whiteList, blackList)
        {
        }

        public override void Tag()
        {
            try
            {
                Regex matchHigherTaxa = new Regex(HigherTaxaMatchPattern);

                var taxaNames = this.XmlDocument.GetNonTaggedTaxa(matchHigherTaxa);

                // Blacklist items
                taxaNames = this.ClearFakeTaxaNames(taxaNames);

                // Whitelist items
                taxaNames = taxaNames.Concat(this.GetTaxaItemsByWhiteList());

                // Select only taxa names which begins with uppercase letter
                taxaNames = taxaNames.Where(s => s[0] == s.ToUpper()[0]).Distinct();

                // TODO: Optimize peformance.
                taxaNames.TagContentInDocument(this.higherTaxaTag, HigherTaxaXPathTemplate, this.XmlDocument, false, true, this.logger);

                this.XmlDocument.RemoveTaxaInWrongPlaces();
            }
            catch
            {
                throw;
            }
        }

        public override void Tag(IXPathProvider xpathProvider)
        {
            this.Tag();
        }
    }
}