namespace ProcessingTools.Bio.Environments.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class EnvoName
    {
        [Key]
        public int Id { get; set; }

        [MinLength(10)]
        [MaxLength(10)]
        public virtual string EnvoEntityId { get; set; }

        public virtual EnvoEntity EnvoEntity { get; set; }

        [Required]
        public string Content { get; set; }
    }
}