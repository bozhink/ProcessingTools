// <copyright file="FloatObjectParseStyleUpdateRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.Floats
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Enumerations.Nlm;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;

    /// <summary>
    /// Float object parse style update request model.
    /// </summary>
    public class FloatObjectParseStyleUpdateRequestModel : IFloatObjectUpdateParseStyleModel, ProcessingTools.Contracts.Models.IWebModel
    {
        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        /// <inheritdoc/>
        [Required]
        public ReferenceType FloatReferenceType { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string Script { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string FloatObjectXPath { get; set; }

        /// <inheritdoc/>
        public Uri ReturnUrl { get; set; }
    }
}
