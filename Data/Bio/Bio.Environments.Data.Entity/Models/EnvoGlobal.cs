namespace ProcessingTools.Bio.Environments.Data.Entity.Models
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Bio.Environments;

    public class EnvoGlobal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [MinLength(ValidationConstants.MinimalLengthOfEnvoGlobalStatus)]
        [MaxLength(ValidationConstants.MaximalLengthOfEnvoGlobalStatus)]
        public string Status { get; set; }
    }
}