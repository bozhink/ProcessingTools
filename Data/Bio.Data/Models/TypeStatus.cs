namespace ProcessingTools.Bio.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ProcessingTools.Bio.Data.Common.Constants;

    public class TypeStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(ValidationConstants.MaximalLengthOfTypeStatusName)]
        public string Name { get; set; }
    }
}