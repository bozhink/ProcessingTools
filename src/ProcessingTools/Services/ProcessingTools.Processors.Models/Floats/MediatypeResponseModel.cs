// <copyright file="MediatypeResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants;
    using ProcessingTools.Processors.Models.Contracts.Floats;

    /// <summary>
    /// Mediatype response model.
    /// </summary>
    public class MediatypeResponseModel : IMediaType
    {
        /// <inheritdoc/>
        public string FileExtension { get; set; }

        /// <inheritdoc/>
        public string MimeType { get; set; } = ContentTypes.DefaultMimetype;

        /// <inheritdoc/>
        public string MimeSubtype { get; set; } = ContentTypes.DefaultMimesubtype;
    }
}
