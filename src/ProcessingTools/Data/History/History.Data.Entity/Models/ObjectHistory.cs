namespace ProcessingTools.History.Data.Entity.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Constants.Data.History;
    using ProcessingTools.Models.Contracts.History;

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

        [Index]
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
