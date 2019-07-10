// <copyright file="TagSpecimenCountCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Tagger;
using ProcessingTools.Contracts.Services.Bio;

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;

    /// <summary>
    /// Tag specimen count command.
    /// </summary>
    [System.ComponentModel.Description("Tag specimen count.")]
    public class TagSpecimenCountCommand : DocumentTaggerCommand<ISpecimenCountTagger>, ITagSpecimenCountCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagSpecimenCountCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="ISpecimenCountTagger"/>.</param>
        public TagSpecimenCountCommand(ISpecimenCountTagger tagger)
            : base(tagger)
        {
        }
    }
}
