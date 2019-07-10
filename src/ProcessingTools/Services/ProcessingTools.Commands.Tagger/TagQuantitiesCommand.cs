// <copyright file="TagQuantitiesCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Tagger;
using ProcessingTools.Contracts.Services.Quantities;

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;

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
