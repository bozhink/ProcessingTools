namespace ProcessingTools.DataResources.Data.Entity.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ProcessingTools.DataResources.Data.Common.Constants;
    using ProcessingTools.DataResources.Data.Common.Models.Contracts;

    public class ContentType : IContentTypeEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(ValidationConstants.ContentTypeNameMaximalLength)]
        public string Name { get; set; }
    }
}
