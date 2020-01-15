// <copyright file="SynonymModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Synonym model.
    /// </summary>
    public abstract class SynonymModel : IGeoSynonym
    {
        /// <summary>
        /// Gets or sets the ID of the synonym.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the parent of the synonym.
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Gets or sets the language code of the synonym.
        /// </summary>
        public int? LanguageCode { get; set; }

        /// <summary>
        /// Gets or sets the name value of the synonym.
        /// </summary>
        public string Name { get; set; }
    }
}
