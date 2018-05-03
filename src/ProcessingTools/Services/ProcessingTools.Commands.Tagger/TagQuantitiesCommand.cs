// <copyright file="TagQuantitiesCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Quantities;

    /// <summary>
    /// Tag quantities command.
    /// </summary>
    [System.ComponentModel.Description("Tag quantities.")]
    public class TagQuantitiesCommand : DocumentTaggerCommand<IQuantitiesTagger>, ITagQuantitiesCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagQuantitiesCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="IQuantitiesTagger"/>.</param>
        public TagQuantitiesCommand(IQuantitiesTagger tagger)
            : base(tagger)
        {
        }
    }
}
