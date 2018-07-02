// <copyright file="IXmlReplaceRuleSetParser.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Rules
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Rules;

    /// <summary>
    /// XML replace rule set parser.
    /// </summary>
    public interface IXmlReplaceRuleSetParser
    {
        /// <summary>
        /// Parses string to <see cref="IXmlReplaceRuleSetModel"/>.
        /// </summary>
        /// <param name="source">Source string to be parsed.</param>
        /// <returns>Rule set.</returns>
        Task<IXmlReplaceRuleSetModel> ParseStringToRuleSetAsync(string source);

        /// <summary>
        /// Parses string to array of <see cref="IXmlReplaceRuleSetModel"/>.
        /// </summary>
        /// <param name="source">Source string to be parsed.</param>
        /// <returns>Array of rule sets.</returns>
        Task<IXmlReplaceRuleSetModel[]> ParseStringToRuleSetsAsync(string source);
    }
}
