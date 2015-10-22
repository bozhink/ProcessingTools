namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Configurator;
    using Globals.Loggers;

    public class SuffixHigherTaxaParser : HigherTaxaParser
    {
        private readonly string[] higherTaxaRanks =
            {
                "phylum",
                "subphylum",
                "class",
                "subclass",
                "superorder",
                "order",
                "suborder",
                "infraorder",
                "superfamily",
                "epifamily",
                "family",
                "subfamily",
                "tribe",
                "subtribe"
            };

        private readonly string[] higherTaxaSuffixes =
            {
                "phyta|mycota",
                "phytina|mycotina",
                "ia|opsida|phyceae|mycetes",
                "idae|phycidae|mycetidae",
                "anae",
                "ales",
                "ineae",
                "aria",
                "acea|oidea",
                "oidae",
                "aceae|idae",
                "oideae|inae",
                "eae|ini",
                "inae|ina"
            };

        private ILogger logger;

        public SuffixHigherTaxaParser(string xml, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
        }

        public SuffixHigherTaxaParser(Config config, string xml, ILogger logger)
            : base(config, xml)
        {
            this.logger = logger;
        }

        public SuffixHigherTaxaParser(IBase baseObject)
            : base(baseObject)
        {
        }

        public override void Parse()
        {
            string patternPrefix = "^([A-Z][a-z]*", patternSuffix = ")$";
            IEnumerable<string> uniqueHigherTaxaList = this.XmlDocument.ExtractUniqueHigherTaxa();
            foreach (string scientificName in uniqueHigherTaxaList)
            {
                this.logger?.Log(scientificName);
                string scientificNameReplacement = scientificName;
                for (int i = 0; i < this.higherTaxaRanks.Length; ++i)
                {
                    string pattern = patternPrefix + this.higherTaxaSuffixes[i] + patternSuffix;
                    string replacement = this.higherTaxaRanks[i].GetRemplacementStringForTaxonNamePartRank();

                    scientificNameReplacement = Regex.Replace(scientificNameReplacement, pattern, replacement);
                }

                this.ReplaceTaxonNameByItsParsedContent(scientificName, scientificNameReplacement);
            }
        }
    }
}
