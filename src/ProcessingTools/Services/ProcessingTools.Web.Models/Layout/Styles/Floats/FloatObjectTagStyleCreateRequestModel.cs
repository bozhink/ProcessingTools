// <copyright file="FloatObjectTagStyleCreateRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.Floats
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Enumerations.Nlm;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.Floats;

    /// <summary>
    /// Float object tag style create request model.
    /// </summary>
    public class FloatObjectTagStyleCreateRequestModel : IFloatObjectInsertTagStyleModel, ProcessingTools.Models.Contracts.IWebModel
    {
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
        public string FloatTypeNameInLabel { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string MatchCitationPattern { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string InternalReferenceType { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string ResultantReferenceType { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string FloatObjectXPath { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string TargetXPath { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
