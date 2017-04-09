namespace ProcessingTools.DataResources.Data.Entity.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Constants.Data.DataResources;
    using ProcessingTools.Contracts.Data.DataResources.Models;

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
