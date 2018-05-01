// <copyright file="IStringReplaceRuleModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Rules
{
    /// <summary>
    /// String replace rule model.
    /// </summary>
    public interface IStringReplaceRuleModel : IRuleModel
    {
        /// <summary>
        /// Gets the replace pattern.
        /// </summary>
        string Pattern { get; }

        /// <summary>
        /// Gets the replace replacement.
        /// </summary>
        string Replacement { get; }
    }
}
