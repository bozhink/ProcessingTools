// <copyright file="MimetypePair.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Files
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Mime-type-pair entity.
    /// </summary>
    public class MimetypePair
    {
        /// <summary>
        /// Gets or sets the ID of the mime-type-pair entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the mime-type entity.
        /// </summary>
        public virtual int MimetypeId { get; set; }

        /// <summary>
        /// Gets or sets the mime-type entity.
        /// </summary>
        public virtual Mimetype Mimetype { get; set; }

        /// <summary>
        /// Gets or sets the ID of the mime-subtype entity.
        /// </summary>
        public virtual int MimesubtypeId { get; set; }

        /// <summary>
        /// Gets or sets the mime-subtype entity.
        /// </summary>
        public virtual Mimesubtype Mimesubtype { get; set; }

        /// <summary>
        /// Gets the collection of file extensions.
        /// </summary>
        public virtual ICollection<FileExtension> FileExtensions { get; private set; } = new HashSet<FileExtension>();
    }
}
