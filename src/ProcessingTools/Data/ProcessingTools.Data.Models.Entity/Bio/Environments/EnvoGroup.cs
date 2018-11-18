namespace ProcessingTools.Data.Models.Entity.Bio.Environments
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Bio.Environments;

    public class EnvoGroup
    {
        [Key]
        public int Id { get; set; }

        [MinLength(ValidationConstants.MinimalLengthOfEnvoEntityId)]
        [MaxLength(ValidationConstants.MaximalLengthOfEnvoEntityId)]
        public string EnvoEntityId { get; set; }

        [MinLength(ValidationConstants.MinimalLengthOfEnvoGroupId)]
        [MaxLength(ValidationConstants.MaximalLengthOfEnvoGroupId)]
        public string EnvoGroupId { get; set; }
    }
}