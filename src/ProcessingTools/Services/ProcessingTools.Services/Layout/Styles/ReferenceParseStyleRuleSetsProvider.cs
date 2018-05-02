// <copyright file="ReferenceParseStyleRuleSetsProvider.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Layout.Styles
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts.Rules;
    using ProcessingTools.Services.Contracts.Layout.Styles;

    /// <summary>
    /// Reference parse style rule sets provider.
    /// </summary>
    public class ReferenceParseStyleRuleSetsProvider : IReferenceParseStyleRuleSetsProvider
    {
        /// <inheritdoc/>
        public IEnumerable<IXmlReplaceRuleSetModel> RuleSets { get; set; }
    }
}
