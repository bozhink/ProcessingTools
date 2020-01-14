// <copyright file="EnvoName.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Bio.Environments
{
    /// <summary>
    /// ENVO name.
    /// </summary>
    public class EnvoName
    {
        /// <summary>
        /// Gets or sets the ID of the ENVO name.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the value of the ENVO name.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the ID of the related ENVO entity.
        /// </summary>
        public virtual string EntityId { get; set; }

        /// <summary>
        /// Gets or sets the related ENVO entity.
        /// </summary>
        public virtual EnvoEntity Entity { get; set; }
    }
}
