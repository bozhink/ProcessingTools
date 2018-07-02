// <copyright file="IPublisherBaseModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Publishers
{
    /// <summary>
    /// Publisher base model.
    /// </summary>
    public interface IPublisherBaseModel
    {
        /// <summary>
        /// Gets the publisher's name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the publisher's abbreviated name.
        /// </summary>
        string AbbreviatedName { get; }

        /// <summary>
        /// Gets the publisher's address string.
        /// </summary>
        string Address { get; }
    }
}
