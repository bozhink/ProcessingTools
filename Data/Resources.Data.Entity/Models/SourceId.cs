namespace ProcessingTools.Resources.Data.Entity.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ProcessingTools.Resources.Data.Common.Constants;
    using ProcessingTools.Resources.Data.Common.Models.Contracts;

    public class SourceId : ISourceIdEntity
    {
        [Key]
        [Required]
        [MaxLength(ValidationConstants.SourceIdMaximalLength)]
        public string Id { get; set; }

        [NotMapped]
        string ISourceIdEntity.SourceId => this.Id;
    }
}
