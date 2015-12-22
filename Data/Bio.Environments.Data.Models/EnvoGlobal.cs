namespace ProcessingTools.Bio.Environments.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class EnvoGlobal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [MinLength(1)]
        [MaxLength(1)]
        public string Status { get; set; }
    }
}