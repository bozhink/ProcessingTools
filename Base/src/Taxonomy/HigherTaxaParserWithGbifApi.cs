namespace ProcessingTools.Base.Taxonomy
{
    using Json.Gbif;
    using System.Collections.Generic;

    public class HigherTaxaParserWithGbifApi : HigherTaxaParser
    {
        public HigherTaxaParserWithGbifApi(string xml)
            : base(xml)
        {
        }

        public HigherTaxaParserWithGbifApi(Config config, string xml)
            : base(config, xml)
        {
        }

        public HigherTaxaParserWithGbifApi(IBase baseObject)
            : base(baseObject)
        {
        }

        public override void Parse()
        {
            List<string> uniqueHigherTaxaList = this.XmlDocument.ExtractUniqueHigherTaxa();

            foreach (string scientificName in uniqueHigherTaxaList)
            {
                this.Delay();

                GbifResult gbifResult = Net.SearchGbif(scientificName);
                if (gbifResult != null)
                {
                    Alert.Log("\n{0} .... {1} .... {2}", scientificName, gbifResult.scientificName, gbifResult.canonicalName);

                    if (gbifResult.canonicalName != null || gbifResult.scientificName != null)
                    {
                        if (!gbifResult.canonicalName.Equals(scientificName) && !gbifResult.scientificName.Contains(scientificName))
                        {
                            Alert.Log("No match.");
                        }
                        else
                        {
                            string rank = gbifResult.rank;
                            if (rank != null && rank != string.Empty)
                            {
                                rank = rank.ToLower();
                                Alert.Log(scientificName + "--> " + rank);

                                string scientificNameReplacement = rank.GetRemplacementStringForTaxonNamePartRank();

                                this.ReplaceTaxonNameByItsParsedContent(scientificName, scientificNameReplacement);
                            }
                        }
                    }
                }
            }
        }
    }
}
