// <copyright file="TypeStatus.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Bio
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Bio;

    /// <summary>
    /// Type status entity.
    /// </summary>
    public class TypeStatus
    {
        /// <summary>
        /// Gets or sets the ID of the type status entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the type status.
        /// </summary>
        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfTypeStatusName)]
        public string Name { get; set; }
    }
}
