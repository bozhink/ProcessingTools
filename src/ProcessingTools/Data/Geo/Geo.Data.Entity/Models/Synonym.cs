namespace ProcessingTools.Geo.Data.Entity.Models
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Geo;
    using ProcessingTools.Models.Contracts;

    public abstract class Synonym : SystemInformation, INameableIntegerIdentifiable, ISynonym, IDataModel
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfGeoName)]
        [MaxLength(ValidationConstants.MaximalLengthOfGeoName)]
        public string Name { get; set; }

        public int? LanguageCode { get; set; }
    }
}