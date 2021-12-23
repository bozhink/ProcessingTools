﻿// <copyright file="IMaterialCitationsParser.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio
{
    using System.Threading.Tasks;

    /// <summary>
    /// Material citations parser.
    /// </summary>
    public interface IMaterialCitationsParser
    {
        /// <summary>
        /// Parse string content.
        /// </summary>
        /// <param name="content">Content to be parsed.</param>
        /// <returns>Content with tagged items.</returns>
        Task<string> ParseAsync(string content);
    }
}
