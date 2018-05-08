// <copyright file="IXmlReplaceRuleSetModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Rules
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
