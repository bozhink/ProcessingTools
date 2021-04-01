﻿// <copyright file="TagEnvironmentTermsWithExtractCommand.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Services.Bio.EnvironmentTerms;

    /// <summary>
    /// Tag environment terms with EXTRACT command.
    /// </summary>
    [System.ComponentModel.Description("Tag envo terms using EXTRACT.")]
    public class TagEnvironmentTermsWithExtractCommand : DocumentTaggerCommand<IEnvironmentTermsWithExtractTagger>, ITagEnvironmentTermsWithExtractCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagEnvironmentTermsWithExtractCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="IEnvironmentTermsWithExtractTagger"/>.</param>
        public TagEnvironmentTermsWithExtractCommand(IEnvironmentTermsWithExtractTagger tagger)
            : base(tagger)
        {
        }
    }
}
