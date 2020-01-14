// <copyright file="IArticleJournalModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Documents.Articles
{
    /// <summary>
    /// Article journal model.
    /// </summary>
    public interface IArticleJournalModel : IStringIdentified
    {
        /// <summary>
        /// Gets or sets the journal's abbreviated name.
        /// </summary>
        string AbbreviatedName { get; set; }

        /// <summary>
        /// Gets or sets the journal's name.
        /// </summary>
        string Name { get; set; }
    }
}
