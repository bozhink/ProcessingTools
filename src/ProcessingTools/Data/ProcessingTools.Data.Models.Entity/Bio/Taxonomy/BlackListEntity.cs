// <copyright file="BlackListEntity.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Bio.Taxonomy
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;
    using ProcessingTools.Common.Constants.Data.Bio.Taxonomy;

    /// <summary>
    /// Black list entity.
    /// </summary>
    public class BlackListEntity : IBlackListItem
    {
        /// <summary>
        /// Gets or sets the ID of the black list entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the content of the black list entity.
        /// </summary>
        [MaxLength(ValidationConstants.MaximalLengthOfBlackListedItemContent)]
        public string Content { get; set; }
    }
}
