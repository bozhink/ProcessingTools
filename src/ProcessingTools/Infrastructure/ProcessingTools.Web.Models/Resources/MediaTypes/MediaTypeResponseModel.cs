// <copyright file="MediaTypeResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Resources.MediaTypes
{
    using ProcessingTools.Models.Contracts.Files.Mediatypes;

    /// <summary>
    /// Represents response model for the media-types API.
    /// </summary>
    public class MediaTypeResponseModel : IMediatype
    {
        /// <summary>
        /// Gets or sets the file extension.
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets the mime-type.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the mime-subtype.
        /// </summary>
        public string MimeSubtype { get; set; }
    }
}
