// <copyright file="TagAbbreviationsCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Tagger;
using ProcessingTools.Contracts.Services.Abbreviations;

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;

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
