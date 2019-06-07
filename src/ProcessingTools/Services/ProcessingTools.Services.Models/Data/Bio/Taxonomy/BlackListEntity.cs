// <copyright file="BlackListEntity.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

    /// <summary>
    /// Black list entity.
    /// </summary>
    public class BlackListEntity : IBlackListItem
    {
        /// <inheritdoc/>
        public string Content { get; set; }
    }
}
