// <copyright file="XmlReplaceRuleSetModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Tests.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// XML replace rule set model.
    /// </summary>
    internal class XmlReplaceRuleSetModel
    {
        /// <summary>
        /// Gets or sets the XPath.
        /// </summary>
        public string XPath { get; set; }

        /// <summary>
        /// Gets or sets the replace rules.
        /// </summary>
        public IEnumerable<ReplaceRuleModel> Rules { get; set; }
    }
}
