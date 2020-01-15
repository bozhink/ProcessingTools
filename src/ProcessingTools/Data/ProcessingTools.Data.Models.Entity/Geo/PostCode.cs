// <copyright file="PostCode.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Geo;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Post code entity.
    /// </summary>
    public class PostCode : BaseModel, IIntegerIdentified
    {
        /// <summary>
        /// Gets or sets the ID of the post code entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the value of the post code.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfPostCode)]
        [MaxLength(ValidationConstants.MaximalLengthOfPostCode)]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the post code type.
        /// </summary>
        public PostCodeType Type { get; set; }

        /// <summary>
        /// Gets or sets the ID of the city entity.
        /// </summary>
        public virtual int CityId { get; set; }

        /// <summary>
        /// Gets or sets the city entity.
        /// </summary>
        public virtual City City { get; set; }
    }
}
