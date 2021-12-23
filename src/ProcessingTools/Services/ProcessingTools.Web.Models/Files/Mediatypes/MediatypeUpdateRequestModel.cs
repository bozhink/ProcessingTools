﻿// <copyright file="MediatypeUpdateRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Files.Mediatypes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Mediatypes;
    using ProcessingTools.Contracts.Models.Files.Mediatypes;

    /// <summary>
    /// Mediatype update request model.
    /// </summary>
    public class MediatypeUpdateRequestModel : IMediatypeUpdateModel, ProcessingTools.Contracts.Models.IWebModel
    {
        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfFileExtensionName)]
        public string Extension { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfMimeTypeName)]
        public string MimeType { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfMimeSubtypeName)]
        public string MimeSubtype { get; set; }

        /// <inheritdoc/>
        [MaxLength(ValidationConstants.MaximalLengthOfFileExtensionDescription)]
        public string Description { get; set; }

        /// <inheritdoc/>
        public Uri ReturnUrl { get; set; }
    }
}
