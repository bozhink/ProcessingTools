namespace ProcessingTools.Geo.Data.Entity.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Constants.Data.Geo;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts;

    public class PostCode : SystemInformation, IIntegerIdentifiable, IDataModel
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = false)]
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfPostCode)]
        [MaxLength(ValidationConstants.MaximalLengthOfPostCode)]
        public string Code { get; set; }

        public PostCodeType Type { get; set; }

        public virtual int CityId { get; set; }

        public virtual City City { get; set; }
    }
}
