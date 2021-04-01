// <copyright file="IStringItemDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models
{
    /// <summary>
    /// String item data transfer object (DTO).
    /// </summary>
    public interface IStringItemDataTransferObject
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        string Content { get; set; }
    }
}
