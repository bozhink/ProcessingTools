// <copyright file="IWebModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    using System;

    /// <summary>
    /// Web model.
    /// </summary>
    public interface IWebModel
    {
        /// <summary>
        /// Gets or sets the return-URL.
        /// </summary>
        Uri ReturnUrl { get; set; }
    }
}
