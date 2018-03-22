// <copyright file="ArticleEditRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Articles
{
    /// <summary>
    /// Article Edit Request Model
    /// </summary>
    public class ArticleEditRequestModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
