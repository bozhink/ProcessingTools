namespace ProcessingTools.DataResources.Data.Entity.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Common.Constants.Data.DataResources;
    using ProcessingTools.Models.Contracts.Resources;

    public class Product : EntityWithSources, IProductEntity
    {
        private string name;

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required(AllowEmptyStrings = false)]
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
