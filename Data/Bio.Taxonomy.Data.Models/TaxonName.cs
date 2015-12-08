namespace ProcessingTools.Bio.Taxonomy.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TaxonName
    {
        private bool whiteListed;

        /// <summary>
        /// Creates new TaxonName object.
        /// </summary>
        public TaxonName()
        {
            this.WhiteListed = false;
        }

        /// <summary>
        /// Gets or sets the key field of the TaxonName.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the value of the taxona name.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Index(IsUnique = true)]
        [MaxLength(60)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the condition if this TaxonName must appear
        /// in the white list for taxon tagging.
        /// </summary>
        public bool WhiteListed
        {
            get
            {
                return this.whiteListed;
            }

            set
            {
                this.whiteListed = value;
            }
        }

        public virtual int TaxonRankId { get; set; }

        public virtual TaxonRank Rank { get; set; }
    }
}
