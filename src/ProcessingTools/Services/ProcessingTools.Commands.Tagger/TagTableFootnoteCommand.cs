// <copyright file="TagTableFootnoteCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Tagger;
using ProcessingTools.Contracts.Services.Floats;

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;

    /// <summary>
    /// Tag table footnote command.
    /// </summary>
    [System.ComponentModel.Description("Tag table foot-notes.")]
    public class TagTableFootnoteCommand : XmlContextTaggerCommand<ITableFootnotesTagger>, ITagTableFootnoteCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagTableFootnoteCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="ITableFootnotesTagger"/>.</param>
        public TagTableFootnoteCommand(ITableFootnotesTagger tagger)
            : base(tagger)
        {
        }
    }
}
