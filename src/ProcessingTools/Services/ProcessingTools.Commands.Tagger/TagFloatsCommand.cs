// <copyright file="TagFloatsCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Tagger;
using ProcessingTools.Contracts.Services.Floats;

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;

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
