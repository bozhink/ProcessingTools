﻿// <copyright file="ICodesTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Contracts.Models;

namespace ProcessingTools.Contracts.Services.Bio.Codes
{
    /// <summary>
    /// Codes tagger.
    /// </summary>
    public interface ICodesTagger
    {
        /// <summary>
        /// Tag known specimen codes.
        /// </summary>
        /// <param name="document"><see cref="IDocument"/> context for tagging.</param>
        /// <returns>Task.</returns>
        Task TagKnownSpecimenCodesAsync(IDocument document);

        /// <summary>
        /// Tag specimen codes.
        /// </summary>
        /// <param name="document"><see cref="IDocument"/> context for tagging.</param>
        /// <returns>Task.</returns>
        Task TagSpecimenCodesAsync(IDocument document);
    }
}
