// <copyright file="IWebModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Web model.
    /// </summary>
    public interface IWebModel
    {
        /// <summary>
        /// Gets or sets the return-URL.
        /// </summary>
        string ReturnUrl { get; set; }
    }
}
