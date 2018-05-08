// <copyright file="TagInstitutionalCodesCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.Codes;

    /// <summary>
    /// Tag institutional codes command.
    /// </summary>
    [System.ComponentModel.Description("Tag institutional codes.")]
    public class TagInstitutionalCodesCommand : DocumentTaggerCommand<IInstitutionalCodesTagger>, ITagInstitutionalCodesCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagInstitutionalCodesCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="IInstitutionalCodesTagger"/>.</param>
        public TagInstitutionalCodesCommand(IInstitutionalCodesTagger tagger)
            : base(tagger)
        {
        }
    }
}
