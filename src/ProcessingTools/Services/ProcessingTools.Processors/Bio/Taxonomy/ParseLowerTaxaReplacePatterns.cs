// <copyright file="ParseLowerTaxaReplacePatterns.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Bio.Taxonomy
{
    /// <summary>
    /// Parse lower taxa replace patterns.
    /// </summary>
    internal static class ParseLowerTaxaReplacePatterns
    {
        private const string GenusPattern = @"[A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+";
        private const string SubgenusPattern = @"[A-Z][a-zçäöüëïâěôûêîæœ\.-]+";
        private const string SpeciesPattern = @"[A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+";
        private const string InfraspecificPattern = SpeciesPattern;
        private const string SuperspeciesPattern = SpeciesPattern;
        private const string SpeciesPatternStrict = @"[a-zçäöüëïâěôûêîæœ\.-]+";
        private const string InfraspecificPatternStrict = SpeciesPatternStrict;

        private const string InternalSignsPattern = @"[\s\?×]*";
        private const string InternalSignsPatternStrict = @"[\s\?×]+";

        /// <summary>
        /// Gets patter-replace pairs.
        /// </summary>
        public static string[,] Replaces => new string[,]
        {
            // Genus species subspecies
            {
                $"({GenusPattern})({InternalSignsPatternStrict})({SpeciesPattern})({InternalSignsPatternStrict})({InfraspecificPattern})",
                @"<tn-part type=""genus"">$1</tn-part>$2<tn-part type=""species"">$3</tn-part>$4<tn-part type=""subspecies"">$5</tn-part>"
            },

            // Genus species
            {
                $"({GenusPattern})({InternalSignsPatternStrict})({SpeciesPattern})",
                @"<tn-part type=""genus"">$1</tn-part>$2<tn-part type=""species"">$3</tn-part>"
            },
            {
                $"‘({GenusPattern})’({InternalSignsPatternStrict})({SpeciesPattern})",
                @"‘<tn-part type=""genus"">$1</tn-part>’$2<tn-part type=""species"">$3</tn-part>"
            },

            // Genus (Subgenus) species subspecies
            {
                $"({GenusPattern})({InternalSignsPattern})\\(\\s*({SubgenusPattern})({InternalSignsPattern}?)\\s*\\)({InternalSignsPattern})({SpeciesPattern})({InternalSignsPatternStrict})({InfraspecificPattern})",
                @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""subgenus"">$3</tn-part>$4)$5<tn-part type=""species"">$6</tn-part>$7<tn-part type=""subspecies"">$8</tn-part>"
            },

            // Genus (superspecies) species subspecies
            {
                $"({GenusPattern})({InternalSignsPattern})\\(\\s*({SuperspeciesPattern})({InternalSignsPattern}?)\\s*\\)({InternalSignsPattern})({SpeciesPattern})({InternalSignsPatternStrict})({InfraspecificPattern})",
                @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""superspecies"">$3</tn-part>$4)$5<tn-part type=""species"">$6</tn-part>$7<tn-part type=""subspecies"">$8</tn-part>"
            },

            // Genus (Subgenus) species
            {
                $"({GenusPattern})({InternalSignsPattern})\\(\\s*({SubgenusPattern})({InternalSignsPattern}?)\\s*\\)({InternalSignsPattern})({SpeciesPattern})",
                @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""subgenus"">$3</tn-part>$4)$5<tn-part type=""species"">$6</tn-part>"
            },

            // Genus (superspecies) species
            {
                $"({GenusPattern})({InternalSignsPattern})\\(\\s*({SuperspeciesPattern})({InternalSignsPattern}?)\\s*\\)({InternalSignsPattern})({SpeciesPattern})",
                @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""superspecies"">$3</tn-part>$4)$5<tn-part type=""species"">$6</tn-part>"
            },

            // Genus (Subgenus)
            {
                $"({GenusPattern})({InternalSignsPattern})\\(\\s*({SubgenusPattern})({InternalSignsPattern}?)\\s*\\)",
                @"<tn-part type=""genus"">$1</tn-part>$2(<tn-part type=""subgenus"">$3</tn-part>$4)"
            },

            // Genus
            {
                $"({GenusPattern})",
                @"<tn-part type=""genus"">$1</tn-part>"
            },

            // species subspecies
            {
                $"({SpeciesPatternStrict})\\s+({InfraspecificPatternStrict})",
                @"<tn-part type=""species"">$1</tn-part> <tn-part type=""subspecies"">$2</tn-part>"
            },

            // species × species
            {
                $"({SpeciesPatternStrict})({InternalSignsPatternStrict})({SpeciesPatternStrict})",
                @"<tn-part type=""species"">$1</tn-part>$2<tn-part type=""species"">$3</tn-part>"
            },

            // species
            {
                $"({SpeciesPatternStrict})",
                @"<tn-part type=""species"">$1</tn-part>"
            },

            // species
            {
                @"([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Ss]sp|[Ss]ubsp)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)",
                @"<tn-part type=""species"">$1</tn-part> <tn-part type=""infraspecific-rank"">$2</tn-part>$3<tn-part type=""subspecies"">$4</tn-part>"
            },

            // species
            {
                @"([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Vv]ar|[Vv]|[Vv]ariety)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)",
                @"<tn-part type=""species"">$1</tn-part> <tn-part type=""infraspecific-rank"">$2</tn-part>$3<tn-part type=""variety"">$4</tn-part>"
            },

            // species
            {
                @"([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Aa]b)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)",
                @"<tn-part type=""species"">$1</tn-part> <tn-part type=""infraspecific-rank"">$2</tn-part>$3<tn-part type=""aberration"">$4</tn-part>"
            },

            // species
            {
                @"([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Ff]|[Ff]orma?)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)",
                @"<tn-part type=""species"">$1</tn-part> <tn-part type=""infraspecific-rank"">$2</tn-part>$3<tn-part type=""form"">$4</tn-part>"
            },

            // species
            {
                @"([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Ss]f|[Ss]ubforma?)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)",
                @"<tn-part type=""species"">$1</tn-part> <tn-part type=""infraspecific-rank"">$2</tn-part>$3<tn-part type=""subform"">$4</tn-part>"
            }
        };
    }
}
