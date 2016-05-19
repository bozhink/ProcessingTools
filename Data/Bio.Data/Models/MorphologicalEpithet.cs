namespace ProcessingTools.Bio.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ProcessingTools.Bio.Data.Common.Constants;

    public class MorphologicalEpithet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(ValidationConstants.MaximalLengthOfMorphologicalEpithetName)]
        public string Name { get; set; }
    }
}