namespace ProcessingTools.Bio.Data.Entity.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Common.Constants.Data.Bio;

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
