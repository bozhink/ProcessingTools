// <copyright file="XmlReplaceRuleSetModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Rules
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts.Rules;

    /// <summary>
    /// XML replace rule set model.
    /// </summary>
    public class XmlReplaceRuleSetModel : IXmlReplaceRuleSetModel
    {
        /// <summary>
        /// Gets or sets the XPath.
        /// </summary>
        public string XPath { get; set; }

        /// <summary>
        /// Gets or sets the replace rules.
        /// </summary>
        public IEnumerable<StringReplaceRuleModel> Rules { get; set; }

        /// <inheritdoc/>
        IEnumerable<IStringReplaceRuleModel> IRuleSetModel<IStringReplaceRuleModel>.Rules => this.Rules;
    }
}
