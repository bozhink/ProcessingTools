// <copyright file="StringReplaceRuleModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Rules
{
    using ProcessingTools.Contracts.Models.Rules;

    /// <summary>
    /// String replace rule model.
    /// </summary>
    public class StringReplaceRuleModel : IStringReplaceRuleModel
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
