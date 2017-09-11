// <copyright file="TaxaRegexPatterns.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Constants
{
    /// <summary>
    /// Regular expressions related to taxonomy.
    /// </summary>
    public static class TaxaRegexPatterns
    {
        /// <summary>
        /// Higher taxa match pattern.
        /// </summary>
        public const string HigherTaxaMatchPattern = @"\b([A-Z](?i)[a-z]*(?:morphae?|mida|toda|ideae|oida|genea|formes|formea|ales|lifera|ieae|indeae|eriae|idea|aceae|oidea|oidae|inae|ini|ina|anae|ineae|acea|oideae|mycota|mycotina|mycetes|mycetidae|phyta|phytina|opsida|phyceae|idae|phycidae|ptera|poda|phaga|itae|odea|alia|ntia|osauria))\b";
    }
}
