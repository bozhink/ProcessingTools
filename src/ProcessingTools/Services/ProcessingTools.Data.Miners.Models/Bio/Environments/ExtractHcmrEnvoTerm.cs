// <copyright file="ExtractHcmrEnvoTerm.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Models.Bio.Environments
{
    using ProcessingTools.Data.Miners.Models.Contracts.Bio.Environments;

    /// <summary>
    /// EXTRACT HCMR ENVO teem.
    /// </summary>
    public class ExtractHcmrEnvoTerm : IExtractHcmrEnvoTerm
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the types.
        /// </summary>
        public int[] Types { get; set; }

        /// <summary>
        /// Gets or sets the identifiers.
        /// </summary>
        public string[] Identifiers { get; set; }
    }
}
