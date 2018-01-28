// <copyright file="PublishersIndexViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Publishers
{
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Publishers Index View Model
    /// </summary>
    public class PublishersIndexViewModel
    {
        /// <summary>
        /// Gets or sets the User Context.
        /// </summary>
        public UserContext UserContext { get; set; }

        /// <summary>
        /// Gets or sets publishers.
        /// </summary>
        public PublisherIndexViewModel[] Publishers { get; set; }
    }
}
