﻿// <copyright file="CommandSettings.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Commands.Models;

    /// <summary>
    /// Command settings.
    /// </summary>
    public class CommandSettings : ICommandSettings
    {
        /// <inheritdoc/>
        public bool ExtractHigherTaxa { get; set; } = false;

        /// <inheritdoc/>
        public bool ExtractLowerTaxa { get; set; } = false;

        /// <inheritdoc/>
        public bool ExtractTaxa { get; set; } = false;

        /// <inheritdoc/>
        public IList<string> FileNames { get; set; } = new List<string>();

        /// <inheritdoc/>
        public string OutputFileName { get; set; }
    }
}