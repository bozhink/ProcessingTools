namespace ProcessingTools.Bio.Environments.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    // TODO: This is not well-defined model.
    public class EnvoGroup
    {
        [Key]
        public int Id { get; set; }

        [MinLength(10)]
        [MaxLength(10)]
        public string EnvoEntityId { get; set; }

        [MinLength(10)]
        [MaxLength(10)]
        public string EnvoGroupId { get; set; }
    }
}