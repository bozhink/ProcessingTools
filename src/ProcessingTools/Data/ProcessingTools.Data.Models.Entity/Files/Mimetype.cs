// <copyright file="Mimetype.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Files
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Mediatypes;

    /// <summary>
    /// Mime-type entity.
    /// </summary>
    public class Mimetype
    {
        private string name;

        /// <summary>
        /// Gets or sets the ID of Mime-type entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the mime-type.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfMimeTypeName)]
        public string Name { get => this.name; set => this.name = value?.ToLowerInvariant(); }

        /// <summary>
        /// Gets the collection of mime-type-pair entities.
        /// </summary>
        public virtual ICollection<MimetypePair> MimeTypePairs { get; private set; } = new HashSet<MimetypePair>();
    }
}
