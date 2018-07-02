// <copyright file="ErrorViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Models
{
    /// <summary>
    /// Error view model.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets the request ID.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Gets a value indicating whether to show the request ID.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
    }
}
