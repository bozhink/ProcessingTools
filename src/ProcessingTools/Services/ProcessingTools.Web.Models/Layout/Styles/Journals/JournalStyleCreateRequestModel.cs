// <copyright file="JournalStyleCreateRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.Journals
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.Journals;

    /// <summary>
    /// Journal style create request model.
    /// </summary>
    public class JournalStyleCreateRequestModel : IJournalInsertStyleModel, ProcessingTools.Models.Contracts.IWebModel
    {
        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        /// <inheritdoc/>
        public IList<string> FloatObjectParseStyleIds { get; set; } = new List<string>();

        /// <inheritdoc/>
        public IList<string> FloatObjectTagStyleIds { get; set; } = new List<string>();

        /// <inheritdoc/>
        public IList<string> ReferenceParseStyleIds { get; set; } = new List<string>();

        /// <inheritdoc/>
        public IList<string> ReferenceTagStyleIds { get; set; } = new List<string>();

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
