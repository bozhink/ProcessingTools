// <copyright file="IValueModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Memory.Integration.Tests.Models
{
    /// <summary>
    /// Value model.
    /// </summary>
    internal interface IValueModel
    {
        /// <summary>
        /// Gets the ID of the model.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the content of the model.
        /// </summary>
        string Content { get; }
    }
}
