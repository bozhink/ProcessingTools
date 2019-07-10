﻿// <copyright file="ParseReferencesCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Models;
using ProcessingTools.Contracts.Commands.Tagger;

namespace ProcessingTools.Commands.Tagger
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Parse references command.
    /// </summary>
    [System.ComponentModel.Description("Parse references.")]
    public class ParseReferencesCommand : IParseReferencesCommand
    {
        /// <inheritdoc/>
        public Task<object> RunAsync(IDocument document, ICommandSettings settings)
        {
            throw new NotImplementedException();
        }
    }
}
