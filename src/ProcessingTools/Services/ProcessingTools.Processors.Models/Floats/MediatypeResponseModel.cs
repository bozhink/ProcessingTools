// <copyright file="MediatypeResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Processors.Models.Contracts.Floats;

    /// <summary>
    /// Mediatype response model.
    /// </summary>
    public class MediatypeResponseModel : IMediaType
    {
        /// <inheritdoc/>
        public string FileExtension { get; set; }

        /// <inheritdoc/>
        public string MimeType { get; set; } = ContentTypes.DefaultMimeType;

        /// <inheritdoc/>
        public string MimeSubtype { get; set; } = ContentTypes.DefaultMimeSubtype;
    }
}
