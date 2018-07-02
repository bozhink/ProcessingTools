// <copyright file="EnvoTermResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Bio.EnvoTerms
{
    using ProcessingTools.Models.Contracts.Bio;

    /// <summary>
    /// Represents response model for the envo terms API.
    /// </summary>
    public class EnvoTermResponseModel : IEnvoTerm
    {
        /// <summary>
        /// Gets or sets the entity ID.
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// Gets or sets the envo ID.
        /// </summary>
        public string EnvoId { get; set; }

        /// <summary>
        /// Gets or sets the content of the term.
        /// </summary>
        public string Content { get; set; }
    }
}
