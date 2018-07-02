// <copyright file="TagMorphologicalEpithetsCommand.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Bio;

    /// <summary>
    /// Tag morphological epithets command.
    /// </summary>
    [System.ComponentModel.Description("Tag morphological epithets.")]
    public class TagMorphologicalEpithetsCommand : DocumentTaggerCommand<IMorphologicalEpithetsTagger>, ITagMorphologicalEpithetsCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagMorphologicalEpithetsCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="IMorphologicalEpithetsTagger"/>.</param>
        public TagMorphologicalEpithetsCommand(IMorphologicalEpithetsTagger tagger)
            : base(tagger)
        {
        }
    }
}
