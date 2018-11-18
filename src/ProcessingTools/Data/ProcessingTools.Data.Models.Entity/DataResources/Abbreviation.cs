namespace ProcessingTools.Data.Models.Entity.DataResources
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Common.Constants.Data.DataResources;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Resources;

    public class Abbreviation : EntityWithSources, IAbbreviation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.AbbreviationNameMaximalLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ValidationConstants.AbbreviationDefinitionMaximalLength)]
        public string Definition { get; set; }

        public virtual int? ContentTypeId { get; set; }

        public virtual ContentType ContentType { get; set; }

        [NotMapped]
        string IContentTyped.ContentType => this.ContentType.Name;
    }
}
