// <copyright file="SpeciesPartsPrefixesResolver.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using ProcessingTools.Enumerations;

    /// <summary>
    /// Species parts prefixes resolver.
    /// </summary>
    internal static class SpeciesPartsPrefixesResolver
    {
        /// <summary>
        /// Gets species parts ranks.
        /// </summary>
        public static readonly IDictionary<string, SpeciesPartType> SpeciesPartsRanks = new Dictionary<string, SpeciesPartType>
        {
                { "?", SpeciesPartType.Species },
                { "a", SpeciesPartType.Aberration },
                { "ab", SpeciesPartType.Aberration },
                { "aff", SpeciesPartType.Species },
                { "afn", SpeciesPartType.Species },
                { "cf", SpeciesPartType.Species },
                { "f", SpeciesPartType.Form },
                { "fo", SpeciesPartType.Form },
                { "fa", SpeciesPartType.Form },
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
                { "secc", SpeciesPartType.Section },
                { "sect", SpeciesPartType.Section },
                { "section", SpeciesPartType.Section },
                { "ser", SpeciesPartType.Series },
                { "series", SpeciesPartType.Series },
                { "sf", SpeciesPartType.Subform },
                { "subf", SpeciesPartType.Subform },
                { "subfo", SpeciesPartType.Subform },
                { "subfa", SpeciesPartType.Subform },
                { "subform", SpeciesPartType.Subform },
                { "subforma", SpeciesPartType.Subform },
                { "sg", SpeciesPartType.Subgenus },
                { "sp", SpeciesPartType.Species },
                { "sp cf", SpeciesPartType.Species },
                { "sp. cf", SpeciesPartType.Species },
                { "sp aff", SpeciesPartType.Species },
                { "sp. aff", SpeciesPartType.Species },
                { "sp nr", SpeciesPartType.Species },
                { "sp. nr", SpeciesPartType.Species },
                { "sp near", SpeciesPartType.Species },
                { "sp. near", SpeciesPartType.Species },
                { "ssp", SpeciesPartType.Subspecies },
                { "sbsp", SpeciesPartType.Subspecies },
                { "st", SpeciesPartType.Stage },
                { "subg", SpeciesPartType.Subgenus },
                { "subgen", SpeciesPartType.Subgenus },
                { "subgenus", SpeciesPartType.Subgenus },
                { "subsec", SpeciesPartType.Subsection },
                { "subsecc", SpeciesPartType.Subsection },
                { "subsect", SpeciesPartType.Subsection },
                { "subsection", SpeciesPartType.Subsection },
                { "supersec", SpeciesPartType.Supersection },
                { "supersecc", SpeciesPartType.Supersection },
                { "supersect", SpeciesPartType.Supersection },
                { "supersection", SpeciesPartType.Supersection },
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
            }.ToImmutableDictionary();

        /// <summary>
        /// Gets uncertainty prefixes.
        /// </summary>
        public static readonly IEnumerable<string> UncertaintyPrefixes = new HashSet<string>
        {
            "?",
            "aff",
            "afn",
            "cf",
            "near",
            "nr",
            "sp aff",
            "sp cf",
            "sp near",
            "sp nr",
            "sp",
            "sp. aff",
            "sp. cf",
            "sp. near",
            "sp. nr"
        }.ToImmutableHashSet();

        private const string DefaultRank = "species";

        /// <summary>
        /// Gets non-ambiguous species parts ranks.
        /// </summary>
        public static IEnumerable<KeyValuePair<string, SpeciesPartType>> NonAmbiguousSpeciesPartsRanks => SpeciesPartsRanks.Where(p => p.Key.Length > 1 && p.Key.IndexOf("trib") < 0 && p.Key != "near");

        /// <summary>
        /// Resolves rank as string to <see cref="SpeciesPartType"/>.
        /// </summary>
        /// <param name="infraspecificRank">The infra-specific rank as string.</param>
        /// <returns>Resolved infra-specific rank as <see cref="SpeciesPartType"/>.</returns>
        public static string Resolve(string infraspecificRank)
        {
            string rank;
            try
            {
                rank = SpeciesPartsRanks[infraspecificRank.ToLowerInvariant()].ToString().ToLowerInvariant();
            }
            catch
            {
                rank = DefaultRank;
            }

            return rank;
        }
    }
}
