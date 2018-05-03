// <copyright file="TagTableFootnoteCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Floats;

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
