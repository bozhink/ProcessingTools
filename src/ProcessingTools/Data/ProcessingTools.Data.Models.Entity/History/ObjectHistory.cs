// <copyright file="ObjectHistory.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.History
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.History;
    using ProcessingTools.Contracts.Models.History;

    /// <summary>
    /// Object history entity.
    /// </summary>
    public class ObjectHistory : IObjectHistory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectHistory"/> class.
        /// </summary>
        public ObjectHistory()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the ID of the object history entity.
        /// </summary>
        [Key]
        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfId)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the data payload of the referent object.
        /// </summary>
        [Required]
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the ID of the referent object.
        /// </summary>
        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfObjectId)]
        public string ObjectId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="System.Type"/> of the referent object.
        /// </summary>
        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfObjectType)]
        public string ObjectType { get; set; }

        /// <summary>
        /// Gets or sets the name of the name of the assembly in which is included the referent object.
        /// </summary>
        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfAssemblyName)]
        public string AssemblyName { get; set; }

        /// <summary>
        /// Gets or sets the version of the assembly in which is included the referent object.
        /// </summary>
        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfAssemblyVersion)]
        public string AssemblyVersion { get; set; }

        /// <inheritdoc/>
        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfUserId)]
        public string CreatedBy { get; set; }

        /// <inheritdoc/>
        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
