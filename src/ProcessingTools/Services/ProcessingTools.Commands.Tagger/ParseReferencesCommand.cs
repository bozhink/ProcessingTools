// <copyright file="ParseReferencesCommand.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Commands.Models;
    using ProcessingTools.Contracts.Commands.Tagger;
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
