namespace ProcessingTools.Base.Taxonomy
{
    using System.Collections.Generic;

    public class AboveGenusHigherTaxaParser : HigherTaxaParser
    {
        public AboveGenusHigherTaxaParser(string xml)
            : base(xml)
        {
        }

        public AboveGenusHigherTaxaParser(Config config, string xml)
            : base(config, xml)
        {
        }

        public AboveGenusHigherTaxaParser(IBase baseObject)
            : base(baseObject)
        {
        }

        /// <summary>
        /// This method parses all non-parsed higher taxa by making then of type 'above-genus'
        /// </summary>
        public override void Parse()
        {
            string rank = "above-genus";
            string scientificNameReplacement = rank.GetRemplacementStringForTaxonNamePartRank();

            IEnumerable<string> uniqueHigherTaxaList = this.XmlDocument.ExtractUniqueHigherTaxa();
            foreach (string scientificName in uniqueHigherTaxaList)
            {
                Alert.Log("\n" + scientificName + " --> " + rank);

                this.ReplaceTaxonNameByItsParsedContent(scientificName, scientificNameReplacement);
            }
        }
    }
}
