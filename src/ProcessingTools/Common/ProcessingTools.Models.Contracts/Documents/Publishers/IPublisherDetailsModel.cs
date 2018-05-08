// <copyright file="IPublisherDetailsModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Publishers
{
    /// <summary>
    /// Publisher details model.
    /// </summary>
    public interface IPublisherDetailsModel : IPublisherModel
    {
        /// <summary>
        /// Gets the number of journals.
        /// </summary>
        long NumberOfJournals { get; }
    }
}
