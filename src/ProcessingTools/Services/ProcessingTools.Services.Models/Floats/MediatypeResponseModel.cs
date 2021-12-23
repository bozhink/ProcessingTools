// <copyright file="MediatypeResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Floats
{
    using ProcessingTools.Contracts.Models.Floats;
    using ProcessingTools.Extensions;

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
