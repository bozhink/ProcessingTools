// <copyright file="UpperCaseFilter.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Text
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Filter text tokens to set them to upper-case.
    /// </summary>    
    public class UpperCaseFilter : IFilter
    {
        /// <inheritdoc/>
        public IEnumerable<string> Filter(IEnumerable<string> tokens)
        {
            if (tokens is null)
            {
                throw new ArgumentNullException(nameof(tokens));
            }

            foreach (string token in tokens)
            {
                yield return token.ToUpperInvariant();
            }
        }
    }
}