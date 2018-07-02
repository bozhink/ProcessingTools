// <copyright file="ReferenceParseStyleCreateRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.References
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.References;

    /// <summary>
    /// Reference parse style create request model.
    /// </summary>
    public class ReferenceParseStyleCreateRequestModel : IReferenceInsertParseStyleModel, ProcessingTools.Models.Contracts.IWebModel
    {
        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string Script { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string ReferenceXPath { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
