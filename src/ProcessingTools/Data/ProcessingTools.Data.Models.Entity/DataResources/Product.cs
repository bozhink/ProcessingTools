namespace ProcessingTools.Data.Models.Entity.DataResources
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.DataResources;
    using ProcessingTools.Models.Contracts.Resources;

    public class Product : EntityWithSources, IProductEntity
    {
        private string name;

        [Key]
        public int Id { get; set; }

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
