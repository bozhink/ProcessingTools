namespace ProcessingTools.DataResources.Data.Entity.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Constants.Data.DataResources;
    using ProcessingTools.Contracts.Data.DataResources.Models;

    public class Product : EntityWithSources, IProduct
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
