// <copyright file="TagEnvironmentTermsCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.EnvironmentTerms;

    /// <summary>
    /// Tag environment terms command.
    /// </summary>
    [System.ComponentModel.Description("Tag envo terms using local database.")]
    public class TagEnvironmentTermsCommand : DocumentTaggerCommand<IEnvironmentTermsTagger>, ITagEnvironmentTermsCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagEnvironmentTermsCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="IEnvironmentTermsTagger"/>.</param>
        public TagEnvironmentTermsCommand(IEnvironmentTermsTagger tagger)
            : base(tagger)
        {
        }
    }
}
