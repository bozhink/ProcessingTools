﻿// <copyright file="IPublisherMetaModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Publishers
{
    /// <summary>
    /// Publisher meta model.
    /// </summary>
    public interface IPublisherMetaModel
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