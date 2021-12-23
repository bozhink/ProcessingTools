// <copyright file="Mediatype.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Files
{
    using ProcessingTools.Contracts.Models.Files.Mediatypes;

    /// <summary>
    /// Media-type.
    /// </summary>
    public class Mediatype : IMediatypeBaseModel
    {
        /// <summary>
        /// Gets or sets the description of the media-type.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the extension of the media-type.
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets the mime-subtype of the media-type.
        /// </summary>
        public string MimeSubtype { get; set; }

        /// <summary>
        /// Gets or sets the mime-type of the media-type.
        /// </summary>
        public string MimeType { get; set; }
    }
}
