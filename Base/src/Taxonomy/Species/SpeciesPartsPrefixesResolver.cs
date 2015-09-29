namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;

    public class SpeciesPartsPrefixesResolver
    {
        private const string DefaultRank = "species";

        private readonly Dictionary<string, SpeciesParts> speciesPartsRanks;

        public SpeciesPartsPrefixesResolver()
        {
            this.speciesPartsRanks = new Dictionary<string, SpeciesParts>();

            this.speciesPartsRanks.Add("subgenus", SpeciesParts.Subgenus);
            this.speciesPartsRanks.Add("subgen", SpeciesParts.Subgenus);
            this.speciesPartsRanks.Add("subg", SpeciesParts.Subgenus);
            this.speciesPartsRanks.Add("sg", SpeciesParts.Subgenus);

            this.speciesPartsRanks.Add("section", SpeciesParts.Section);
            this.speciesPartsRanks.Add("sect", SpeciesParts.Section);
            this.speciesPartsRanks.Add("sec", SpeciesParts.Section);

            this.speciesPartsRanks.Add("subsection", SpeciesParts.Subsection);
            this.speciesPartsRanks.Add("subsect", SpeciesParts.Subsection);
            this.speciesPartsRanks.Add("subsec", SpeciesParts.Subsection);

            this.speciesPartsRanks.Add("series", SpeciesParts.Series);
            this.speciesPartsRanks.Add("ser", SpeciesParts.Series);

            this.speciesPartsRanks.Add("subseries", SpeciesParts.Subseries);
            this.speciesPartsRanks.Add("subser", SpeciesParts.Subseries);

            this.speciesPartsRanks.Add("tribe", SpeciesParts.Tribe);
            this.speciesPartsRanks.Add("trib", SpeciesParts.Tribe);

            this.speciesPartsRanks.Add("sp", SpeciesParts.Species);
            this.speciesPartsRanks.Add("gr", SpeciesParts.Species);
            this.speciesPartsRanks.Add("aff", SpeciesParts.Species);
            this.speciesPartsRanks.Add("prope", SpeciesParts.Species);
            this.speciesPartsRanks.Add("cf", SpeciesParts.Species);
            this.speciesPartsRanks.Add("nr", SpeciesParts.Species);
            this.speciesPartsRanks.Add("near", SpeciesParts.Species);
            this.speciesPartsRanks.Add("sp. near", SpeciesParts.Species);
            this.speciesPartsRanks.Add("×", SpeciesParts.Species);
            this.speciesPartsRanks.Add("?", SpeciesParts.Species);

            this.speciesPartsRanks.Add("subspecies", SpeciesParts.Subspecies);
            this.speciesPartsRanks.Add("subspec", SpeciesParts.Subspecies);
            this.speciesPartsRanks.Add("subsp", SpeciesParts.Subspecies);
            this.speciesPartsRanks.Add("ssp", SpeciesParts.Subspecies);

            this.speciesPartsRanks.Add("var", SpeciesParts.Variety);
            this.speciesPartsRanks.Add("v", SpeciesParts.Variety);

            this.speciesPartsRanks.Add("subvar", SpeciesParts.Subvariety);
            this.speciesPartsRanks.Add("sv", SpeciesParts.Subvariety);

            this.speciesPartsRanks.Add("forma", SpeciesParts.Form);
            this.speciesPartsRanks.Add("form", SpeciesParts.Form);
            this.speciesPartsRanks.Add("fo", SpeciesParts.Form);
            this.speciesPartsRanks.Add("f", SpeciesParts.Form);

            this.speciesPartsRanks.Add("sf", SpeciesParts.Subform);

            this.speciesPartsRanks.Add("ab", SpeciesParts.Aberration);
            this.speciesPartsRanks.Add("a", SpeciesParts.Aberration);

            this.speciesPartsRanks.Add("st", SpeciesParts.Stage);

            this.speciesPartsRanks.Add("r", SpeciesParts.Rank);
        }

        public string Resolve(string infraSpecificRank)
        {
            string rank;
            try
            {
                rank = this.speciesPartsRanks[infraSpecificRank.ToLower()].ToString().ToLower();
            }
            catch
            {
                rank = DefaultRank;
            }

            return rank;
        }
    }
}
