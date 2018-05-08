// <copyright file="TagAbbreviationsCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Abbreviations;

    /// <summary>
    /// Tag abbreviations command.
    /// </summary>
    [System.ComponentModel.Description("Tag abbreviations.")]
    public class TagAbbreviationsCommand : XmlContextTaggerCommand<IAbbreviationsTagger>, ITagAbbreviationsCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagAbbreviationsCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="IAbbreviationsTagger"/>.</param>
        public TagAbbreviationsCommand(IAbbreviationsTagger tagger)
            : base(tagger)
        {
        }
    }
}
