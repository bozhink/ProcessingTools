// <copyright file="IPublisherBaseDataModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Contracts.Documents.Publishers
{
    /// <summary>
    /// Publisher base data model.
    /// </summary>
    public interface IPublisherBaseDataModel
    {
        /// <summary>
        /// Gets or sets the publisher's abbreviated name.
        /// </summary>
        string AbbreviatedName { get; set; }

        /// <summary>
        /// Gets or sets the publisher's name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the publisher's address string.
        /// </summary>
        string Address { get; set; }
    }
}
