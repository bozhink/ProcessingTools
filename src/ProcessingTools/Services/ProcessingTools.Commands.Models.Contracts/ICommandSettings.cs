// <copyright file="ICommandSettings.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Models.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    /// Command settings.
    /// </summary>
    public interface ICommandSettings
    {
        /// <summary>
        /// Gets a value indicating whether higher taxa have to be extracted.
        /// </summary>
        bool ExtractHigherTaxa { get; }

        /// <summary>
        /// Gets a value indicating whether lower taxa have to be extracted.
        /// </summary>
        bool ExtractLowerTaxa { get; }

        /// <summary>
        /// Gets a value indicating whether taxa have to be extracted.
        /// </summary>
        bool ExtractTaxa { get; }

        /// <summary>
        /// Gets input file names.
        /// </summary>
        IList<string> FileNames { get; }

        /// <summary>
        /// Gets or sets output file name.
        /// </summary>
        string OutputFileName { get; set; }
    }
}
