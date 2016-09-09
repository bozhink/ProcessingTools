namespace ProcessingTools.Resources.Data.Entity.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ProcessingTools.Resources.Data.Common.Constants;
    using ProcessingTools.Resources.Data.Common.Models.Contracts;

    public class Product : EntityWithSources, IProductEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(ValidationConstants.ProductNameMaximalLength)]
        public string Name { get; set; }
    }
}
