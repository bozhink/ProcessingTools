// <copyright file="IReferenceParseStyleRuleSetsProvider.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Layout.Styles
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts.Rules;

    /// <summary>
    /// Reference parse style rule sets provider.
    /// </summary>
    public interface IReferenceParseStyleRuleSetsProvider
    {
        /// <summary>
        /// Gets or sets the rule sets.
        /// </summary>
        IEnumerable<IXmlReplaceRuleSetModel> RuleSets { get; set; }
    }
}
