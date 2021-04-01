// <copyright file="Mimesubtype.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Files
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Mediatypes;

    /// <summary>
    /// Mime-subtype entity.
    /// </summary>
    public class Mimesubtype
    {
        private string name;

        /// <summary>
        /// Gets or sets the ID of the mime-subtype entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the mime-subtype.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfMimeSubtypeName)]
        public string Name { get => this.name; set => this.name = value?.ToLowerInvariant(); }

        /// <summary>
        /// Gets the collection of mime-type-pair entities.
        /// </summary>
        public virtual ICollection<MimetypePair> MimeTypePairs { get; private set; } = new HashSet<MimetypePair>();
    }
}
