// <copyright file="FileExtension.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Files
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Mediatypes;

    /// <summary>
    /// File extension entity.
    /// </summary>
    public class FileExtension
    {
        /// <summary>
        /// Gets or sets the ID of the file extension entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the file extension.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfFileExtensionName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the file extension.
        /// </summary>
        [MaxLength(ValidationConstants.MaximalLengthOfFileExtensionDescription)]
        public string Description { get; set; }

        /// <summary>
        /// Gets the collection of media-types.
        /// </summary>
        public virtual ICollection<MimetypePair> MimetypePairs { get; private set; } = new HashSet<MimetypePair>();
    }
}
