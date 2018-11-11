// <copyright file="MessageStatusResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Shared
{
    using ProcessingTools.Common.Enumerations;

    /// <summary>
    /// API response model.
    /// </summary>
    public class MessageStatusResponseModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether API request is completed successfully.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the status of request execution.
        /// </summary>
        public MessageResponseStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the message of the response.
        /// </summary>
        public string Message { get; set; }
    }
}
