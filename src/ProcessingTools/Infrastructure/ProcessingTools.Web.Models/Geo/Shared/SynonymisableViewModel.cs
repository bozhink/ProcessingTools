// <copyright file="SynonymisableViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.Shared
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Synonymisable view model
    /// </summary>
    public class SynonymisableViewModel
    {
        /// <summary>
        /// Gets or sets synonyms.
        /// </summary>
        [Display(Name = nameof(Strings.Synonyms), ResourceType = typeof(Strings))]
        public IEnumerable<SynonymViewModel> Synonyms { get; set; }
    }
}
