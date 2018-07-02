// <copyright file="TagDatesCommand.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Dates;

    /// <summary>
    /// Tag dates command.
    /// </summary>
    [System.ComponentModel.Description("Tag dates.")]
    public class TagDatesCommand : DocumentTaggerCommand<IDatesTagger>, ITagDatesCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagDatesCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="IDatesTagger"/>.</param>
        public TagDatesCommand(IDatesTagger tagger)
            : base(tagger)
        {
        }
    }
}
