namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Common.Constants.Data.Geo;
    using ProcessingTools.Models.Contracts;

    public abstract class BaseModel : ICreated, IModified
    {
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfUserIdentifier)]
        [MaxLength(ValidationConstants.MaximalLengthOfUserIdentifier)]
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfUserIdentifier)]
        [MaxLength(ValidationConstants.MaximalLengthOfUserIdentifier)]
        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        [Timestamp]
        [Column("ts")]
        public byte[] Timestamp { get; set; }
    }
}
