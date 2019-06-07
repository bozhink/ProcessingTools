﻿namespace ProcessingTools.Data.Models.Entity.History
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.History;
    using ProcessingTools.Contracts.Models.History;

    public class ObjectHistory : IObjectHistory
    {
        public ObjectHistory()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        [Key]
        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfId)]
        public string Id { get; set; }

        [Required]
        public string Data { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfObjectId)]
        public string ObjectId { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfObjectType)]
        public string ObjectType { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfAssemblyName)]
        public string AssemblyName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfAssemblyVersion)]
        public string AssemblyVersion { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfUserId)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
