// <copyright file="IXmlReplaceRuleSetModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Rules
{
    /// <summary>
    /// XML replace rule set model.
    /// </summary>
    public interface IXmlReplaceRuleSetModel : IRuleSetModel<IStringReplaceRuleModel>
    {
        /// <summary>
        /// Gets the XPath.
        /// </summary>
        string XPath { get; }
    }
}
