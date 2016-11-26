namespace ProcessingTools.DataResources.Data.Entity.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.DataResources.Data.Common.Constants;
    using ProcessingTools.DataResources.Data.Common.Contracts.Models;

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
