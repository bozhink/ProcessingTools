﻿// <copyright file="IMaterialCitationsParser.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace ProcessingTools.Contracts.Services.Bio
{
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
