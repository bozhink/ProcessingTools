// <copyright file="BaseModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Common.Constants.Data.Geo;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Base DB entity model.
    /// </summary>
    public abstract class BaseModel : ICreated, IModified
    {
        /// <summary>
        /// Gets or sets the ID of the user which created the record in the DB.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfUserIdentifier)]
        [MaxLength(ValidationConstants.MaximalLengthOfUserIdentifier)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the creation of the record in the DB.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user which modified the record in the DB.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfUserIdentifier)]
        [MaxLength(ValidationConstants.MaximalLengthOfUserIdentifier)]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time of modification of the record in the DB.
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the record.
        /// </summary>
        [Timestamp]
        [Column("ts")]
        public byte[] Timestamp { get; set; }
    }
}
