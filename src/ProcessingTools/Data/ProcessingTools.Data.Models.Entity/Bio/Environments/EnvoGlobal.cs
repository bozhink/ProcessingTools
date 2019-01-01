namespace ProcessingTools.Data.Models.Entity.Bio.Environments
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Bio.Environments;

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