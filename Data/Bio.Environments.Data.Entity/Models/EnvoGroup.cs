namespace ProcessingTools.Bio.Environments.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Bio.Environments.Data.Common.Constants;

    // TODO: This is not well-defined model.
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