// <copyright file="FloatObjectParseStyleUpdateRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.Floats
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Enumerations.Nlm;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.Floats;

    /// <summary>
    /// Float object parse style update request model.
    /// </summary>
    public class FloatObjectParseStyleUpdateRequestModel : IFloatObjectUpdateParseStyleModel, ProcessingTools.Models.Contracts.IWebModel
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
        public string ReturnUrl { get; set; }
    }
}
