// <copyright file="TagFloatsCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Floats;

    /// <summary>
    /// Tag floats command.
    /// </summary>
    [System.ComponentModel.Description("Tag floats.")]
    public class TagFloatsCommand : XmlContextTaggerCommand<IFloatsTagger>, ITagFloatsCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagFloatsCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="IFloatsTagger"/>.</param>
        public TagFloatsCommand(IFloatsTagger tagger)
            : base(tagger)
        {
        }
    }
}
