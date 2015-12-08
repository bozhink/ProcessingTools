namespace ProcessingTools.Bio.Taxonomy.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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
        [Index(IsUnique = true)]
        [MaxLength(15)]
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value?.ToLower();
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