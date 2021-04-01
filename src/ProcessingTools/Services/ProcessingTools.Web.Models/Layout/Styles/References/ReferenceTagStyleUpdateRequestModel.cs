// <copyright file="ReferenceTagStyleUpdateRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.References
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;

    /// <summary>
    /// Reference tag style update request model.
    /// </summary>
    public class ReferenceTagStyleUpdateRequestModel : IReferenceUpdateTagStyleModel, ProcessingTools.Contracts.Models.IWebModel
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
        [Required(AllowEmptyStrings = false)]
        public string Script { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string ReferenceXPath { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string TargetXPath { get; set; }

        /// <inheritdoc/>
        public Uri ReturnUrl { get; set; }
    }
}
