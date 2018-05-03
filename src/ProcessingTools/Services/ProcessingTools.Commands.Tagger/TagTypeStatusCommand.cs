// <copyright file="TagTypeStatusCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Bio;

    /// <summary>
    /// Tag type status command.
    /// </summary>
    [System.ComponentModel.Description("Tag type status.")]
    public class TagTypeStatusCommand : DocumentTaggerCommand<ITypeStatusTagger>, ITagTypeStatusCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagTypeStatusCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="ITypeStatusTagger"/>.</param>
        public TagTypeStatusCommand(ITypeStatusTagger tagger)
            : base(tagger)
        {
        }
    }
}
