// <copyright file="DocumentFileViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Documents
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Document file view model.
    /// </summary>
    public class DocumentFileViewModel
    {
        /// <summary>
        /// Gets or sets the object ID of the file.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "File")]
        public string FileId { get; set; }

        /// <summary>
        /// Gets or sets the content type of the file.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Content type")]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the content length of the file.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Content length")]
        public long ContentLength { get; set; }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        [Display(Name = "File name")]
        public string FileName { get; set; }
    }
}
