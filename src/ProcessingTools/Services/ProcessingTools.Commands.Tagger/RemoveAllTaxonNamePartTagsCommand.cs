// <copyright file="RemoveAllTaxonNamePartTagsCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// Remove all taxon name part tags command.
    /// </summary>
    [System.ComponentModel.Description("Remove all taxon-name-part tags.")]
    public class RemoveAllTaxonNamePartTagsCommand : DocumentFormatterCommand<ITaxonNamePartsRemover>, IRemoveAllTaxonNamePartTagsCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveAllTaxonNamePartTagsCommand"/> class.
        /// </summary>
        /// <param name="formatter">Instance of <see cref="ITaxonNamePartsRemover"/>.</param>
        public RemoveAllTaxonNamePartTagsCommand(ITaxonNamePartsRemover formatter)
            : base(formatter)
        {
        }
    }
}
