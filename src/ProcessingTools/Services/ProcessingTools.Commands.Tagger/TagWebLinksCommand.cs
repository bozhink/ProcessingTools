// <copyright file="TagWebLinksCommand.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Services.ExternalLinks;

    /// <summary>
    /// Tag web links command.
    /// </summary>
    [System.ComponentModel.Description("Tag web links and DOI.")]
    public class TagWebLinksCommand : DocumentTaggerCommand<IExternalLinksTagger>, ITagWebLinksCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagWebLinksCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="IExternalLinksTagger"/>.</param>
        public TagWebLinksCommand(IExternalLinksTagger tagger)
            : base(tagger)
        {
        }
    }
}
