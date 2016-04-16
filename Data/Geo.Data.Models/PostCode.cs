namespace ProcessingTools.Geo.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Geo.Data.Common.Constants;

    public class PostCode
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = false)]
        [MaxLength(ValidationConstants.MaximalLengthOgPostCode)]
        public string Code { get; set; }

        public PostCodeType Type { get; set; }

        public virtual int CityId { get; set; }

        public virtual City City { get; set; }
    }
}