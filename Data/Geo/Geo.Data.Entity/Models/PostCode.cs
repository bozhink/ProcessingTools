namespace ProcessingTools.Geo.Data.Entity.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Constants.Data.Geo;

    public class PostCode
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfPostCode)]
        public string Code { get; set; }

        public PostCodeType Type { get; set; }

        public virtual int CityId { get; set; }

        public virtual City City { get; set; }
    }
}
