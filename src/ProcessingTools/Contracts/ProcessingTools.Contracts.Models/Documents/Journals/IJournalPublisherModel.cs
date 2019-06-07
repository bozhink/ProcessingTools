// <copyright file="IJournalPublisherModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Documents.Journals
{
    /// <summary>
    /// Journal publisher model.
    /// </summary>
    public interface IJournalPublisherModel : IStringIdentifiable
    {
        /// <summary>
        /// Gets the publisher's name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the publisher's abbreviated name.
        /// </summary>
        string AbbreviatedName { get; }
    }
}
