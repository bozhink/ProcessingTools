// <copyright file="IArticleJournalModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Articles
{
    /// <summary>
    /// Article journal model.
    /// </summary>
    public interface IArticleJournalModel : IStringIdentifiable
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
