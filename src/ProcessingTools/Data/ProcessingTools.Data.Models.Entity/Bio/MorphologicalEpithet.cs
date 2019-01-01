namespace ProcessingTools.Data.Models.Entity.Bio
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Bio;

    public class MorphologicalEpithet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfMorphologicalEpithetName)]
        public string Name { get; set; }
    }
}
