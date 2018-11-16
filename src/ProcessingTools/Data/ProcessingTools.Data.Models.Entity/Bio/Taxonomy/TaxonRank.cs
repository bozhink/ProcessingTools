namespace ProcessingTools.Data.Models.Entity.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Bio.Taxonomy;

    public class TaxonRank
    {
        private string name;
        private ICollection<TaxonName> taxa;

        public TaxonRank()
        {
            this.taxa = new HashSet<TaxonName>();
        }

        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfRankName)]
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value?.ToLowerInvariant();
            }
        }

        public virtual ICollection<TaxonName> Taxa
        {
            get
            {
                return this.taxa;
            }

            set
            {
                this.taxa = value;
            }
        }
    }
}
