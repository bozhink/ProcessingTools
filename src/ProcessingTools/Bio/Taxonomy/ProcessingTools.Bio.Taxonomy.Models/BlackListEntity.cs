// <copyright file="BlackListEntity.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Models
{
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;

    /// <summary>
    /// Black list entity.
    /// </summary>
    public class BlackListEntity : IBlackListItem
    {
        /// <inheritdoc/>
        public string Content { get; set; }
    }
}
