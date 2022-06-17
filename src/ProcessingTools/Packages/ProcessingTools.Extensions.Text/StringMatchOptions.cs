// <copyright file="StringMatchOptions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Text
{
    /// <summary>
    /// String match options.
    /// </summary>
    public class StringMatchOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether match should be treated as regex.
        /// </summary>
        public bool IsRegEx { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether match should be case-sensitive.
        /// </summary>
        public bool CaseSensitive { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether whole-word-match should be applied.
        /// </summary>
        public bool MatchWholeWord { get; set; } = false;
    }
}
