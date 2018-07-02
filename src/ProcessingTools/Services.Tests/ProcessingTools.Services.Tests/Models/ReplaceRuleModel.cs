// <copyright file="ReplaceRuleModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Tests.Models
{
    /// <summary>
    /// Replace rule model.
    /// </summary>
    internal class ReplaceRuleModel
    {
        /// <summary>
        /// Gets or sets the replace pattern.
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Gets or sets the replace replacement.
        /// </summary>
        public string Replacement { get; set; }
    }
}
