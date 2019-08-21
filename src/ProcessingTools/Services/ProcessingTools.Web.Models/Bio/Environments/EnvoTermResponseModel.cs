// <copyright file="EnvoTermResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Bio.Environments
{
    using ProcessingTools.Contracts.Models.Bio.Environments;

    /// <summary>
    /// Represents response model for the ENVO terms API.
    /// </summary>
    public class EnvoTermResponseModel : IEnvoTerm
    {
        /// <summary>
        /// Gets or sets the entity ID.
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// Gets or sets the ENVO ID.
        /// </summary>
        public string EnvoId { get; set; }

        /// <summary>
        /// Gets or sets the content of the term.
        /// </summary>
        public string Content { get; set; }
    }
}
