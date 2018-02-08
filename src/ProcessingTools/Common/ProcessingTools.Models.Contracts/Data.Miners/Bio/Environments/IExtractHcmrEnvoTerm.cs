﻿// <copyright file="IExtractHcmrEnvoTerm.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Contracts.Models.Bio.Environments
{
    /// <summary>
    /// EXTRACT HCMR envo-term.
    /// </summary>
    public interface IExtractHcmrEnvoTerm
    {
        /// <summary>
        /// Gets the content.
        /// </summary>
        string Content { get; }

        /// <summary>
        /// Gets the identifiers.
        /// </summary>
        string[] Identifiers { get; }

        /// <summary>
        /// Gets the types.
        /// </summary>
        int[] Types { get; }
    }
}
