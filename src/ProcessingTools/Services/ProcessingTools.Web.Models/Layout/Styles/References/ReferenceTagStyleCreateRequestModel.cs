﻿// <copyright file="ReferenceTagStyleCreateRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Layout.Styles.References;

namespace ProcessingTools.Web.Models.Layout.Styles.References
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Reference tag style create request model.
    /// </summary>
    public class ReferenceTagStyleCreateRequestModel : IReferenceInsertTagStyleModel, ProcessingTools.Contracts.Models.IWebModel
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
        [Required(AllowEmptyStrings = false)]
        public string TargetXPath { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
