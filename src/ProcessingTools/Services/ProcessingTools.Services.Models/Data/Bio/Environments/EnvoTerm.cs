// <copyright file="EnvoTerm.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Bio.Environments
{
    using ProcessingTools.Contracts.Models.Services.Data.Bio.Environments;

    /// <summary>
    /// ENVO Term service model.
    /// </summary>
    public class EnvoTerm : IEnvoTerm
    {
        /// <inheritdoc/>
        public string EntityId { get; set; }

        /// <inheritdoc/>
        public string EnvoId { get; set; }

        /// <inheritdoc/>
        public string Content { get; set; }
    }
}
