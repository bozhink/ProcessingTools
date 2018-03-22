// <copyright file="ArticleCreateRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Articles
{
    /// <summary>
    /// Article Create Request Model
    /// </summary>
    public class ArticleCreateRequestModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
