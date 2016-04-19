namespace ProcessingTools.Bio.Taxonomy
{
    using System.Collections.Generic;
    using Types;

    public class SpeciesPartsPrefixesResolver
    {
        public static readonly IDictionary<string, SpeciesPartType> SpeciesPartsRanks = new Dictionary<string, SpeciesPartType>
        {
                { "?", SpeciesPartType.Species },
                { "a", SpeciesPartType.Aberration },
                { "ab", SpeciesPartType.Aberration },
                { "aff", SpeciesPartType.Species },
                { "cf", SpeciesPartType.Species },
                { "f", SpeciesPartType.Form },
                { "fo", SpeciesPartType.Form },
                { "form", SpeciesPartType.Form },
                { "forma", SpeciesPartType.Form },
                { "mod", SpeciesPartType.Form },
                { "gr", SpeciesPartType.Species },
                { "near", SpeciesPartType.Species },
                { "nr", SpeciesPartType.Species },
                { "prope", SpeciesPartType.Species },
                { "r", SpeciesPartType.Race },
                { "race", SpeciesPartType.Race },
                { "rassa", SpeciesPartType.Race },
                { "sec", SpeciesPartType.Section },
                { "sect", SpeciesPartType.Section },
                { "section", SpeciesPartType.Section },
                { "ser", SpeciesPartType.Series },
                { "series", SpeciesPartType.Series },
                { "sf", SpeciesPartType.Subform },
                { "sg", SpeciesPartType.Subgenus },
                { "sp", SpeciesPartType.Species },
                { "sp. near", SpeciesPartType.Species },
                { "ssp", SpeciesPartType.Subspecies },
                { "sbsp", SpeciesPartType.Subspecies },
                { "st", SpeciesPartType.Stage },
                { "subg", SpeciesPartType.Subgenus },
                { "subgen", SpeciesPartType.Subgenus },
                { "subgenus", SpeciesPartType.Subgenus },
                { "subsec", SpeciesPartType.Subsection },
                { "subsect", SpeciesPartType.Subsection },
                { "subsection", SpeciesPartType.Subsection },
                { "subser", SpeciesPartType.Subseries },
                { "subseries", SpeciesPartType.Subseries },
                { "subsp", SpeciesPartType.Subspecies },
                { "subspec", SpeciesPartType.Subspecies },
                { "subspecies", SpeciesPartType.Subspecies },
                { "subvar", SpeciesPartType.Subvariety },
                { "sv", SpeciesPartType.Subvariety },
                { "trib", SpeciesPartType.Tribe },
                { "tribe", SpeciesPartType.Tribe },
                { "v", SpeciesPartType.Variety },
                { "var", SpeciesPartType.Variety },
                { "×", SpeciesPartType.Species }
            };

        private const string DefaultRank = "species";

        public string Resolve(string infraSpecificRank)
        {
            string rank;
            try
            {
                rank = SpeciesPartsPrefixesResolver.SpeciesPartsRanks[infraSpecificRank.ToLower()].ToString().ToLower();
            }
            catch
            {
                rank = DefaultRank;
            }

            return rank;
        }
    }
}