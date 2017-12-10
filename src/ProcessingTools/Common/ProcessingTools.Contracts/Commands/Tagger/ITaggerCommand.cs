// <copyright file="ITaggerCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Commands.Tagger
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Commands;

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
        Task<object> Run(IDocument document, ICommandSettings settings);
    }
}
