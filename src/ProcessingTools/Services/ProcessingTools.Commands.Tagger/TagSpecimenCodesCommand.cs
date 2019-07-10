// <copyright file="TagSpecimenCodesCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Tagger;
using ProcessingTools.Contracts.Services.Bio.Codes;

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;

    /// <summary>
    /// Tag specimen codes command.
    /// </summary>
    [System.ComponentModel.Description("Tag specimen codes.")]
    public class TagSpecimenCodesCommand : DocumentTaggerCommand<ISpecimenCodesByPatternTagger>, ITagSpecimenCodesCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagSpecimenCodesCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="ISpecimenCodesByPatternTagger"/>.</param>
        public TagSpecimenCodesCommand(ISpecimenCodesByPatternTagger tagger)
            : base(tagger)
        {
        }
    }
}
