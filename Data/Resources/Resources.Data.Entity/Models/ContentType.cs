namespace ProcessingTools.Resources.Data.Entity.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Resources.Data.Common.Constants;
    using ProcessingTools.Resources.Data.Common.Contracts.Models;

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
