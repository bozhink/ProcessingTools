// <copyright file="ITaggerCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Contracts.Commands.Models;
using ProcessingTools.Contracts.Models;

namespace ProcessingTools.Contracts.Commands.Tagger
{
    /// <summary>
    /// Tagger Command.
    /// </summary>
    public interface ITaggerCommand
    {
        /// <summary>
        /// Executes command.
        /// </summary>
        /// <param name="document"><see cref="IDocument"/> context for the command.</param>
        /// <param name="settings"><see cref="ICommandSettings"/> for execution.</param>
        /// <returns>Task of result.</returns>
        Task<object> RunAsync(IDocument document, ICommandSettings settings);
    }
}
