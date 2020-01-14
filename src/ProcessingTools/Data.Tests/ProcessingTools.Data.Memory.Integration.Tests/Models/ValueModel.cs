// <copyright file="ValueModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Memory.Integration.Tests.Models
{
    /// <summary>
    /// Value model.
    /// </summary>
    internal class ValueModel : IValueModel
    {
        /// <summary>
        /// Gets or sets the ID of the model.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the content of the model.
        /// </summary>
        public string Content { get; set; }
    }
}
