namespace ProcessingTools.DataResources.Data.Entity.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.DataResources.Data.Common.Constants;
    using ProcessingTools.DataResources.Data.Common.Models.Contracts;

    public class Product : EntityWithSources, IProductEntity
    {
        private string name;

        [Key]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(ValidationConstants.ProductNameMaximalLength)]
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value.Trim(' ', ',', ';', ':', '/', '\\');
            }
        }
    }
}
