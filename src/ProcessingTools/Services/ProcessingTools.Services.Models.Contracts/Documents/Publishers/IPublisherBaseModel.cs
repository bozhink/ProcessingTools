// <copyright file="IPublisherBaseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Contracts.Documents.Publishers
{
    /// <summary>
    /// Publisher base model.
    /// </summary>
    public interface IPublisherBaseModel
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
