// <copyright file="IWebModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
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
