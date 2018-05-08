// <copyright file="IRuleSetModel{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Rules
{
    using System.Collections.Generic;

    /// <summary>
    /// Generic rule set model.
    /// </summary>
    /// <typeparam name="T">Type of the rule.</typeparam>
    public interface IRuleSetModel<out T> : IRuleSetModel
        where T : IRuleModel
    {
        /// <summary>
        /// Gets the rules.
        /// </summary>
        IEnumerable<T> Rules { get; }
    }
}
