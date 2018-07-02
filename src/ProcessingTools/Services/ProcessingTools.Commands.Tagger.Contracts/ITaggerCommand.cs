// <copyright file="ITaggerCommand.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger.Contracts
{
    using System.Threading.Tasks;
    using ProcessingTools.Commands.Models.Contracts;
    using ProcessingTools.Contracts;

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
