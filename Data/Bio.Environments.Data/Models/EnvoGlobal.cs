namespace ProcessingTools.Bio.Environments.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Bio.Environments.Data.Common.Constants;

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