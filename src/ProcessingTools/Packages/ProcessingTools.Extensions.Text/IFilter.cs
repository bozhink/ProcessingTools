// <copyright file="IFilter.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Text
{
    using System.Collections.Generic;

    /// <summary>
    /// Text token filter.
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Filter string tokens.
        /// </summary>
        /// <param name="tokens">Tokens to be evaluated and filtered.</param>
        /// <returns>Filtered tokens.</returns>
        IEnumerable<string> Filter(IEnumerable<string> tokens);
    }
}