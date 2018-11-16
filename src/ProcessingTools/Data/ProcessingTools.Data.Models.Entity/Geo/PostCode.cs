namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Geo;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Models.Contracts;

    public class PostCode : BaseModel, IIntegerIdentifiable, IDataModel
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfPostCode)]
        [MaxLength(ValidationConstants.MaximalLengthOfPostCode)]
        public string Code { get; set; }

        public PostCodeType Type { get; set; }

        public virtual int CityId { get; set; }

        public virtual City City { get; set; }
    }
}
