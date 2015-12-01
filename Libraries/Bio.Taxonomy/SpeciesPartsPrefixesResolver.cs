namespace ProcessingTools.Bio.Taxonomy
{
    using System.Collections.Generic;
    using Types;

    public class SpeciesPartsPrefixesResolver
    {
        private const string DefaultRank = "species";

        private readonly Dictionary<string, SpeciesPartType> speciesPartsRanks;

        public SpeciesPartsPrefixesResolver()
        {
            this.speciesPartsRanks = new Dictionary<string, SpeciesPartType>();

            this.speciesPartsRanks.Add("subgenus", SpeciesPartType.Subgenus);
            this.speciesPartsRanks.Add("subgen", SpeciesPartType.Subgenus);
            this.speciesPartsRanks.Add("subg", SpeciesPartType.Subgenus);
            this.speciesPartsRanks.Add("sg", SpeciesPartType.Subgenus);

            this.speciesPartsRanks.Add("section", SpeciesPartType.Section);
            this.speciesPartsRanks.Add("sect", SpeciesPartType.Section);
            this.speciesPartsRanks.Add("sec", SpeciesPartType.Section);

            this.speciesPartsRanks.Add("subsection", SpeciesPartType.Subsection);
            this.speciesPartsRanks.Add("subsect", SpeciesPartType.Subsection);
            this.speciesPartsRanks.Add("subsec", SpeciesPartType.Subsection);

            this.speciesPartsRanks.Add("series", SpeciesPartType.Series);
            this.speciesPartsRanks.Add("ser", SpeciesPartType.Series);

            this.speciesPartsRanks.Add("subseries", SpeciesPartType.Subseries);
            this.speciesPartsRanks.Add("subser", SpeciesPartType.Subseries);

            this.speciesPartsRanks.Add("tribe", SpeciesPartType.Tribe);
            this.speciesPartsRanks.Add("trib", SpeciesPartType.Tribe);

            this.speciesPartsRanks.Add("sp", SpeciesPartType.Species);
            this.speciesPartsRanks.Add("gr", SpeciesPartType.Species);
            this.speciesPartsRanks.Add("aff", SpeciesPartType.Species);
            this.speciesPartsRanks.Add("prope", SpeciesPartType.Species);
            this.speciesPartsRanks.Add("cf", SpeciesPartType.Species);
            this.speciesPartsRanks.Add("nr", SpeciesPartType.Species);
            this.speciesPartsRanks.Add("near", SpeciesPartType.Species);
            this.speciesPartsRanks.Add("sp. near", SpeciesPartType.Species);
            this.speciesPartsRanks.Add("×", SpeciesPartType.Species);
            this.speciesPartsRanks.Add("?", SpeciesPartType.Species);

            this.speciesPartsRanks.Add("subspecies", SpeciesPartType.Subspecies);
            this.speciesPartsRanks.Add("subspec", SpeciesPartType.Subspecies);
            this.speciesPartsRanks.Add("subsp", SpeciesPartType.Subspecies);
            this.speciesPartsRanks.Add("ssp", SpeciesPartType.Subspecies);

            this.speciesPartsRanks.Add("var", SpeciesPartType.Variety);
            this.speciesPartsRanks.Add("v", SpeciesPartType.Variety);

            this.speciesPartsRanks.Add("subvar", SpeciesPartType.Subvariety);
            this.speciesPartsRanks.Add("sv", SpeciesPartType.Subvariety);

            this.speciesPartsRanks.Add("forma", SpeciesPartType.Form);
            this.speciesPartsRanks.Add("form", SpeciesPartType.Form);
            this.speciesPartsRanks.Add("fo", SpeciesPartType.Form);
            this.speciesPartsRanks.Add("f", SpeciesPartType.Form);

            this.speciesPartsRanks.Add("sf", SpeciesPartType.Subform);

            this.speciesPartsRanks.Add("ab", SpeciesPartType.Aberration);
            this.speciesPartsRanks.Add("a", SpeciesPartType.Aberration);

            this.speciesPartsRanks.Add("st", SpeciesPartType.Stage);

            this.speciesPartsRanks.Add("race", SpeciesPartType.Race);
            this.speciesPartsRanks.Add("r", SpeciesPartType.Race);
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