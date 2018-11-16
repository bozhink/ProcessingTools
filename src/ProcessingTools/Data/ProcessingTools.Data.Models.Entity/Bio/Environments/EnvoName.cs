namespace ProcessingTools.Data.Models.Entity.Bio.Environments
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Bio.Environments;

    public class EnvoName
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfEnvoNameContent)]
        public string Content { get; set; }

        public virtual string EnvoEntityId { get; set; }

        public virtual EnvoEntity EnvoEntity { get; set; }
    }
}