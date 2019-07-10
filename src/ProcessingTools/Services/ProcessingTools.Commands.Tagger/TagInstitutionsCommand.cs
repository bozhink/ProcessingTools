﻿// <copyright file="TagInstitutionsCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Services.Institutions;

    /// <summary>
    /// Tag institutions command.
    /// </summary>
    [System.ComponentModel.Description("Tag institutions.")]
    public class TagInstitutionsCommand : DocumentTaggerCommand<IInstitutionsTagger>, ITagInstitutionsCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagInstitutionsCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="IInstitutionsTagger"/>.</param>
        public TagInstitutionsCommand(IInstitutionsTagger tagger)
            : base(tagger)
        {
        }
    }
}
